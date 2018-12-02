using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1;

    public ParticleSystem pSystem;

    private Vector3 targetPosition;

    private bool isGhost = false;

    private Collider2D enemyCollider;

    void Start()
    {
        //Fetch the GameObject's Collider (make sure it has a Collider component)
        enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        var targetPos = getTargetPlayer().transform.position;

        if (isGhost) {
            targetPos = targetPosition;

            if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
            {
                Rebirth();
                return;
            }
        }
        
        var diff = targetPos - transform.position;

        // TODO: Maybe could use Vector3.MoveTowards?
        var dir = diff.normalized
            + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    void ChangeOpacity(float opacity) {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = opacity;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    void Rebirth() {
        speed *= 1.5f/20f;
        speed = Mathf.Clamp(speed, 1, 2.5f);
        isGhost = false;
        ChangeOpacity(1f);
        pSystem.Play();
        enemyCollider.enabled = true;
        if (transform.localScale.x > 0.4f && GameObject.FindGameObjectsWithTag("Monster").Length < 45)
        {
            transform.localScale *= 0.7f;
            Instantiate(gameObject);
        }
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

    void Die()
    {
        pSystem.Play();
        isGhost = true;
        enemyCollider.enabled = false;
        ChangeOpacity(0.3f);
        SoundManager.PlaySound("enemyDie");

        // Get randomized position next to the enemy
        targetPosition = new Vector3(
                getOtherPlayer().transform.position.x * Random.Range(0.3f, 0.9f),
                Random.Range(-6f, 6f),
                transform.position.z
            );

        speed *= 20f;
        speed = Mathf.Clamp(speed, 1, 20f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isGhost) {
            return;
        }

        if (col.gameObject.tag == "Bullet")
        {
            var initialBulletX = col.GetComponent<Bullet>().initialPosition.x;
            bool bulletHasCrossed = initialBulletX * col.gameObject.transform.position.x < 0;

            if (bulletHasCrossed)
            {
                return;
            }
            Destroy(col.gameObject);
            Die();
        }

        // This caused the ghost moving bugging, test if it's needed or not
        if (col.gameObject.tag == "Player")
        {
            //SwapAndClone();
        }
    }
}
