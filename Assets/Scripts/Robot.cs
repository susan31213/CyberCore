using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    GameObject[] onlinePlayers;
    int[] playersStatus;

    public Vector3 fMove, lastPos;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        onlinePlayers = GameObject.FindGameObjectsWithTag("Player");
        canJump = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fMove = transform.position - lastPos;
        lastPos = transform.position;
    }

    public GameObject[] GetOnlinePlayers()
    {
        return onlinePlayers;
    }

    public bool getCanJump() { return canJump; }

    public void setCanJump(bool b) { canJump = b; }
}
