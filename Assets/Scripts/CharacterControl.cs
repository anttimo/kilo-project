using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float speed = 1.0f;
    public int playerNumber = 1;

    public Transform firepoint;
    public GameObject bulletPrefab;

    public static Vector3 originLocation;

    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        originLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal" + playerNumber);
        float moveY = Input.GetAxis("Vertical" + playerNumber);

        transform.Translate(new Vector2(moveX * Time.deltaTime * speed, moveY * Time.deltaTime * speed));
        //GetComponent<Rigidbody2D>().AddForce( new Vector2(moveX * speed, moveY * speed) );

        if (moveX != 0 && !spriteRenderer.flipX ? (moveX < 0.01f) : (moveX > 0.01f))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if (Input.GetButtonDown("Fire" + playerNumber))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        int rotation = 180;
        if (playerNumber == 2) rotation = 0;
        Instantiate(bulletPrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("On trigger enter " + col.gameObject.tag);
        if (col.gameObject.tag == "Goal")
        {
            GameManager.instance.Win(playerNumber);
        }
        else if (col.gameObject.tag == "Monster")
        {
            Destroy(col.gameObject);
        }
        // if (col.gameObject.tag == "Bullet") {
        //     Destroy(col.gameObject);
        // }
    }
}
