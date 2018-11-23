using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public float knockBack = 10.0f;
    public Rigidbody2D rb;

	float lifetime = 0.0f;
	// Use this for initialization 
	void Start () {
		rb.velocity = transform.right * speed;
	}

	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > 3) Destroy(gameObject);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.name != "Bullet(Clone)" && col.gameObject.name != "CenterLine") {
			Destroy(gameObject);
			//col.gameObject.transform.Translate( new Vector2(knockBack, 0));
		}
	}
}
