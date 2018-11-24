using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	
    public float speed = 40f;
    public Rigidbody2D rb;

	private List<GameObject> enemiesHit = new List<GameObject>();
	
	void Start()
	{
		rb.velocity = transform.right * speed;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (!enemiesHit.Contains(col.gameObject)) {
			enemiesHit.Add(col.gameObject);
			if (rb.velocity.x > 0) {
				col.gameObject.transform.position = new Vector2(col.gameObject.transform.position.x  + 3f, col.gameObject.transform.position.y);
			} else {
				col.gameObject.transform.position = new Vector2(col.gameObject.transform.position.x  - 3f, col.gameObject.transform.position.y);
			}
		}
	}
}
