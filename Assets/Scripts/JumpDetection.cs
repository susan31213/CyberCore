using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetection : MonoBehaviour
{
    private Robot robot;

    void Start()
    {
        robot = transform.parent.GetComponent<Robot>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            robot.setCanJump(true);
        }

    }
}
