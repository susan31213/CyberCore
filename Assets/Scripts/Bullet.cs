using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4f;
    public Transform gun;
    public Vector3 direction;
    public float lifeTime;
    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifeTimer > lifeTime)
            Destroy(gameObject);
        lifeTimer += Time.deltaTime;
        transform.Translate(direction.normalized * speed, gun);    
    }
}
