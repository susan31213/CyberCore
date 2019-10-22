using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform center;

    public Vector2 YLimit, XLimit;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(center.position.x, center.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(center.position.x, center.position.y, transform.position.z), moveSpeed);
        checkBoundary();
    }

    private void checkBoundary()
    {
        float xx = (transform.position.x > XLimit.x) ? XLimit.x : (transform.position.x < XLimit.y) ? XLimit.y : transform.position.x;
        float yy = (transform.position.y > YLimit.x) ? YLimit.x : (transform.position.y < YLimit.y) ? YLimit.y : transform.position.y;
        transform.position = new Vector3(xx, yy, transform.position.z);
    }
}
