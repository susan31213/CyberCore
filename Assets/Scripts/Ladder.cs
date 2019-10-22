using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Collider2D upperFloor, lowerFloor;
    private void Start()
    {
        for(int i=2; i<=3; i++)
        {
            GameObject f = GameObject.Find("floor_" + i.ToString());
            if(f.transform.position.y > transform.position.y)
            {
                upperFloor = f.GetComponent<Collider2D>();
                lowerFloor = GameObject.Find("floor_" + (i-1).ToString()).GetComponent<Collider2D>();
                break;
            }
        }
    }
    public Collider2D getUpperFloor() { return upperFloor; }
    public Collider2D getLowerFloor() { return lowerFloor; }
}
