using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMove : InnerController
{
    private float speed = 2;
    private float jumpSpeed = 5.5f;
    private float jump_timer, jump_interval = 0.8f;
    
    public override void Use()
    {
        base.Use();
        Rigidbody2D robotRb = robot.GetComponent<Rigidbody2D>();
        float inputHorizontal = (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Left])) ? -1 : (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Right])) ? 1 : 0;
        robotRb.velocity = new Vector2(inputHorizontal * speed, robotRb.velocity.y);
        bodyAnimator.SetFloat("walk", inputHorizontal);
        if (Input.GetKeyDown(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Up]) && robot.getCanJump())
        {
            robot.setCanJump(false);
            robotRb.velocity = new Vector2(robotRb.velocity.x, jumpSpeed);
        }

        // Update player position
        foreach (GameObject player in robot.GetOnlinePlayers())
        {
            Player p = player.GetComponent<Player>();
            if (p != usingPlayer && p.canMove)
            {
                p.rb().position += new Vector2(robot.fMove.x, robot.fMove.y * 0.2f);
            }
        }
    }

    public override void StopUse()
    {
        base.StopUse();
        bodyAnimator.SetFloat("walk", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform p = transform.parent;
        while (p.name != "Robot") p = p.parent;
        robot = p.GetComponent<Robot>();
        bodyAnimator = p.GetChild(0).GetComponent<Animator>();

        jump_timer = jump_interval;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.gameObject == usingPlayer)
            {
                col.GetComponent<Player>().canMove = true;
                usingPlayer = null;
                
            }

        }
    }
}
