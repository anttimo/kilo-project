using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float speed = 1.0f;
    public int playerNumber = 1;

    public Transform firepoint;
    public GameObject bulletPrefab;

    public GameObject shockwavePrefab;

    public static Vector3 originLocation;

    public SpriteRenderer spriteRenderer;
    private float nextFire;
    public float fireDelay;

    private float nextShockwave;
    public float shockwaveDelay = 3f;
    public Rigidbody2D rb;

    public bool knockingBack = false;

    // Use this for initialization
    void Start()
    {
        originLocation = transform.position;
        fireDelay = 0.5f;
        nextFire = 0f;
        nextShockwave = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }

        float moveX = Input.GetAxis("Horizontal" + playerNumber);
        float moveY = Input.GetAxis("Vertical" + playerNumber);

        if (!knockingBack)
        {
            transform.Translate(new Vector2(moveX * Time.deltaTime * speed, moveY * Time.deltaTime * speed));
        }

        if (moveX != 0 && !spriteRenderer.flipX ? (moveX < 0.01f) : (moveX > 0.01f))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if ((Time.time > nextFire) && Input.GetButtonDown("Fire" + playerNumber))
        {
            Shoot();
            nextFire = Time.time + fireDelay;
        }

        if ((Time.time > nextShockwave) && Input.GetButtonDown("Fire2" + playerNumber))
        {
            Shockwave();
            nextShockwave = Time.time + shockwaveDelay;
        }
    }

    void Shoot()
    {
        int rotation = 180;
        if (playerNumber == 2) rotation = 0;
        Instantiate(bulletPrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    void Shockwave()
    {
        int rotation = 180;
        if (playerNumber == 2) rotation = 0;
        Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal")
        {
            GameManager.instance.Win(playerNumber);
        }
        else if (col.gameObject.tag == "Monster")
        {
            StartCoroutine(Knockback());
        }
        if (col.gameObject.tag == "Bullet")
        {
            StartCoroutine(Knockback());
            Destroy(col.gameObject);
        }
    }

    IEnumerator Knockback()
    {
        if (knockingBack)
        {
            Debug.Log("Skip KB");
            yield break;
        }
        Debug.Log("Start KB");
        knockingBack = true;
        rb.AddForce(
            new Vector2(transform.position.x / Mathf.Abs(transform.position.x) * 5, 0),
            ForceMode2D.Impulse
        );
        yield return new WaitForSeconds(1);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        knockingBack = false;
        Debug.Log("Stop KB");
    }
}
