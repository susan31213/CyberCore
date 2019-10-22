using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerController : MonoBehaviour
{
    protected Robot robot;
    public GameObject controllee;
    public Player usingPlayer;

    public Animator bodyAnimator;
    private bool walking;

    public virtual void Use()
    {
    }
    public virtual void StopUse()
    {
        
    }

    void Start()
    {
        Transform p = transform.parent;
        while (p.name != "Robot") p = p.parent;
        robot = p.GetComponent<Robot>();
        bodyAnimator = p.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(col.gameObject == usingPlayer)
            {
                col.GetComponent<Player>().canMove = true;
                usingPlayer = null;
            }
            
        }
    }
}
