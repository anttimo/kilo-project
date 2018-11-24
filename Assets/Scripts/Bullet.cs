using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public float knockBack = 10.0f;
    public Rigidbody2D rb;

    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (transform.position.x * initialPosition.x < 0)
        {
            //Destroy(gameObject);
        }
    }
}
