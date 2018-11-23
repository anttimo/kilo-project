﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public float knockBack = 10.0f;
    public Rigidbody2D rb;
    // Use this for initialization 
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
}
