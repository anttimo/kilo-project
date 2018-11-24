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
        if (Mathf.Abs(transform.position.x) > CameraController.arenaWidth * 2)
        {
            Destroy(gameObject);
        }
    }
}
