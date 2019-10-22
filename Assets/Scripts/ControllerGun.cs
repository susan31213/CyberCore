using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGun : InnerController
{
    public GameObject bullet;
    public float shooting_interval = 0.5f;
    private float shooting_timer = 0;
    private float speed = 2;

    public override void Use()
    {
        base.Use();
        // Set control player position
        Rigidbody2D rb = usingPlayer.GetComponent<Player>().rb();
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Control the gun direction
        Transform gun = controllee.transform.GetChild(0);
        float inputVertical = (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Down])) ? -1 : (Input.GetKey(usingPlayer.GetInputKey()[(int)GameSetting.PlayerInput.Up])) ? 1 : 0;
        gun.Rotate(new Vector3(0, 0, inputVertical * speed));
        float rotation_z = (gun.localRotation.eulerAngles.z > 180f) ? gun.localRotation.eulerAngles.z - 360 : gun.localRotation.eulerAngles.z;
        if (rotation_z > 80) gun.localRotation = Quaternion.Euler(0, 0, 80);
        else if (rotation_z < -80) gun.localRotation = Quaternion.Euler(0, 0, -80);

        // Shoot with time interval
        if (shooting_timer > shooting_interval)
        {
            shooting_timer = 0;
            Transform bullet_shoot_pos = gun.GetChild(0).Find("top");
            GameObject go = Instantiate(bullet, bullet_shoot_pos.position, bullet_shoot_pos.rotation);
            Bullet b = go.GetComponent<Bullet>();
            b.gun = transform;
            b.direction = bullet_shoot_pos.position - gun.transform.position;
            b.lifeTime = 5;
        }
        else
        {
            shooting_timer += Time.fixedDeltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform p = transform.parent;
        while (p.name != "Robot") p = p.parent;
        robot = p.GetComponent<Robot>();
        bodyAnimator = p.GetChild(0).GetComponent<Animator>();
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
