using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 0.5f;
    public bool frozen = false;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.instance.paused || frozen)
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

    IEnumerator SwapAndClone()
    {

        frozen = true;
        transform.position = new Vector3(
                getOtherPlayer().transform.position.x * Random.Range(0.5f, 1f),
                Random.Range(-10f, 10f),
                transform.position.z);

        speed *= 1.3f;
        speed = Mathf.Clamp(speed, 0.25f, 0.75f);

        if (transform.localScale.x > 0.5f)
        {
            transform.localScale *= 0.6f;
            GetComponent<Rigidbody2D>().mass *= 0.6f;
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

            //bool destroy = isPlayer1 ? getTargetPlayer() == GameManager.instance.player1 : getTargetPlayer() == GameManager.instance.player2;
            if (bulletHasCrossed)
            {
                return;
            }

            Destroy(col.gameObject);
            StartCoroutine(SwapAndClone());
        }

        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(SwapAndClone());
        }
    }
}
