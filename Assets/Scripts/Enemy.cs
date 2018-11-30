﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1;
    public bool frozen = false;

    public ParticleSystem pSystem;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.paused || frozen)
        {
            return;
        }
        var playerPos = getTargetPlayer().transform.position;
        var diff = playerPos - transform.position;
        // if (diff.x * playerPos.x < 0 && playerPos.x * transform.position.x > 0 && Mathf.Abs(diff.x) > 2)
        // {
        //     frozen = true;
        //     StartCoroutine(SwapAndClone());
        //     return;
        // }
        var dir = diff.normalized
            + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        transform.position += dir.normalized * speed * Time.deltaTime;
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

    IEnumerator SwapAndClone()
    {
        transform.position = new Vector3(
                getOtherPlayer().transform.position.x * Random.Range(0.3f, 0.9f),
                Random.Range(-6f, 6f),
                transform.position.z
            );

        speed *= 1.5f;
        speed = Mathf.Clamp(speed, 1, 2.5f);

        if (transform.localScale.x > 0.4f && GameObject.FindGameObjectsWithTag("Monster").Length < 45)
        {
            transform.localScale *= 0.7f;
            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
            frozen = false;
            Instantiate(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.25f, 0.75f));
            frozen = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            var initialBulletX = col.GetComponent<Bullet>().initialPosition.x;
            bool bulletHasCrossed = initialBulletX * col.gameObject.transform.position.x < 0;

            if (bulletHasCrossed)
            {
                return;
            }

            pSystem.Play();
            Destroy(col.gameObject);
            frozen = true;
            StartCoroutine(SwapAndClone());
            SoundManager.PlaySound("enemyDie");
        }

        if (col.gameObject.tag == "Player")
        {
            frozen = true;
            StartCoroutine(SwapAndClone());
        }
    }
}
