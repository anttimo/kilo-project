﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 0.5f;
    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        transform.position += (getTargetPlayer().transform.position - transform.position) * speed * Time.deltaTime;
    }

    private GameObject getTargetPlayer()
    {
        if (GameManager.instance.player1.transform.position.x * transform.position.x > 0)
        {
            return GameManager.instance.player1;
        }
        return GameManager.instance.player2;
    }

    private GameObject getOtherPlayer()
    {
        if (getTargetPlayer() == GameManager.instance.player1)
        {
            return GameManager.instance.player2;
        }
        return GameManager.instance.player1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            var initialBulletX = col.GetComponent<Bullet>().initialPosition.x;
            bool bulletHasCrossed = initialBulletX * col.gameObject.transform.position.x < 0;

            //bool destroy = isPlayer1 ? getTargetPlayer() == GameManager.instance.player1 : getTargetPlayer() == GameManager.instance.player2;
            if (bulletHasCrossed)
            {
                return;
            }

            Destroy(col.gameObject);

            transform.position = new Vector3(
                getOtherPlayer().transform.position.x * Random.Range(0.2f, 0.4f),
                getOtherPlayer().transform.position.y * Random.Range(0.5f, 1.5f),
                transform.position.z);

            speed *= 2;
            speed = Mathf.Clamp(speed, 0.5f, 2f);

            if (transform.localScale.x > 0.2f)
            {
                transform.localScale *= 0.8f;
                GetComponent<Rigidbody2D>().mass *= 0.8f;
            }

            Instantiate(gameObject);
        }
    }
}
