using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (player.transform.position - transform.position) * 0.1f * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag != "Bullet")
        {
            Destroy(col.gameObject);
            transform.position = new Vector3(
                -transform.position.x,
                transform.position.y,
                transform.position.z);
        }

    }
}
