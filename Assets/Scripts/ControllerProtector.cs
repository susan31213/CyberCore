using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BezierSolution
{
    public class ControllerProtector : InnerController
    {
        private Vector3 originRot;
        public override void Use()
        {
            base.Use();
            // Set control player position
            Rigidbody2D rb = usingPlayer.GetComponent<Player>().rb();
            rb.velocity = new Vector2(0, rb.velocity.y);

            // Control the protector direction
            Transform protector = controllee.transform;
            BezierWalkerWithSpeed walker = protector.GetComponent<BezierWalkerWithSpeed>();
            float inputVertical = (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Left])) ? -1 : (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Right])) ? 1 : 0;
            walker.speed = inputVertical;
            //// get the angle
            Vector3 norTar = (robot.transform.position - protector.position).normalized;
            float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
            //// rotate to angle
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);
            protector.rotation = rotation;
        }

        public override void StopUse()
        {
            base.StopUse();
            Transform protector = controllee.transform;
            BezierWalkerWithSpeed walker = protector.GetComponent<BezierWalkerWithSpeed>();
            walker.speed = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            Transform p = transform.parent;
            while (p.name != "Robot") p = p.parent;
            robot = p.GetComponent<Robot>();
            bodyAnimator = p.GetChild(0).GetComponent<Animator>();

            originRot = robot.transform.position - robot.transform.Find("Foot").position;
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
}
