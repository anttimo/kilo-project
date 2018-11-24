using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	
    public float speed = 40f;
    public float knockBack = 10.0f;
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
			col.gameObject.transform.position = new Vector2(col.gameObject.transform.position.x  * 0.95f, col.gameObject.transform.position.y);
		}
	}
}
