using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    public bool canMove;
    public float speed;
    private Rigidbody2D rig;
    private float inputHorizontal;
    private float inputVertical;
    private KeyCode[] inputKey;
    private Vector3 beforeUsingPos, lastRobotPos;

    public float rayCastDistance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    public Collider2D ignoreCol;

    void Start()
    {
        canMove = true;
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);

        rig = GetComponent<Rigidbody2D>();
        inputKey = new KeyCode[GameSetting.numOfPlayerInput];
        for(int i=0; i<GameSetting.numOfPlayerInput; i++)
        {
            inputKey[i] = GameSetting.keyList[id, i];
        }
    }

    
    void FixedUpdate()
    {
        
        // Player movement
        if (canMove)
        {
            inputHorizontal = (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Left])) ? -1f : (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Right])) ? 1f : 0;
            rig.velocity = new Vector2(inputHorizontal * speed, rig.velocity.y);

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, rayCastDistance, whatIsLadder);
            if (hitInfo.collider != null)
            {
                ignoreCol = hitInfo.collider.GetComponent<Ladder>().getUpperFloor();
                if (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Up]) || Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Down])) isClimbing = true;
            }
            else
            {
                isClimbing = false;
                if (ignoreCol != null)
                {
                    Physics2D.IgnoreCollision(ignoreCol, GetComponent<Collider2D>(), false);
                    ignoreCol = null;
                }
            }

            if (isClimbing)
            {
                inputVertical = (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Down])) ? -1f : (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Up])) ? 1f : 0;
                rig.velocity = new Vector2(rig.velocity.x, inputVertical * speed);
                rig.gravityScale = 0;
                Physics2D.IgnoreCollision(ignoreCol, GetComponent<Collider2D>(), true);
            }
            else
            {
                rig.gravityScale = 1;
            }
        }
        else
        {
            
        }
        //transform.position = rig.position;
        //rig.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // Controller detect
        if (col.gameObject.layer == 11)
        {
            //canMove = !Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Interact]);
            InnerController controller = col.GetComponent<InnerController>();
            if (Input.GetKey(inputKey[(int)GameSetting.PlayerInput.Interact]))
            {
                canMove = false;
                if (controller.usingPlayer == null)
                {
                    //beforeUsingPos = transform.localPosition;
                    controller.usingPlayer = this;
                }
                controller.Use();
                transform.position = col.transform.position;
            }
            if(Input.GetKeyUp(inputKey[(int)GameSetting.PlayerInput.Interact]))
            {
                if(controller != null)controller.usingPlayer = null;
                controller.StopUse();
                canMove = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //if (col.gameObject.layer == 11)
        //{
        //    canMove = true;
        //    InnerController controller = col.GetComponent<InnerController>();
        //    if(controller.usingPlayer == col.gameObject)
        //        controller.usingPlayer = null;
        //}
    }

    public KeyCode[] GetInputKey() { return this.inputKey; }
    public Rigidbody2D rb() { return this.rig; }
}
