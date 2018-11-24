using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        transform.position += (getTargetPlayer().transform.position - transform.position) * 0.1f * Time.deltaTime;
    }

    private GameObject getTargetPlayer()
    {
        if (GameManager.instance.player1.transform.position.x * transform.position.x > 0)
        {
            return GameManager.instance.player1;
        }
        return GameManager.instance.player2;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            transform.position = new Vector3(
                -transform.position.x,
                transform.position.y,
                transform.position.z);
            // TODO: Take player from GameManager and switch the target to it
        }

    }
}
