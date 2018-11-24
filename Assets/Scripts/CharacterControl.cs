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

    public GameObject forcefieldPrefab;

    private GameObject forcefield;

    public static Vector3 originLocation;

    public SpriteRenderer spriteRenderer;
    private float nextFire;
    public float fireDelay;

    private float nextShockwave;
    public float shockwaveDelay = 3f;

    public float forcefieldDestroy;

    public float forcefieldTime = 2f;

    public float nextForcefield;

    public float forcefieldDelay = 8f;

    // Use this for initialization
    void Start()
    {
        originLocation = transform.position;
        fireDelay = 0.5f;
        nextFire = 0f;
        nextShockwave = 0f;
        forcefieldDestroy = 0f;
        nextForcefield = 0f;
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

        transform.Translate(new Vector2(moveX * Time.deltaTime * speed, moveY * Time.deltaTime * speed));
        //GetComponent<Rigidbody2D>().AddForce( new Vector2(moveX * speed, moveY * speed) );

        if (moveX != 0 && !spriteRenderer.flipX ? (moveX < 0.01f) : (moveX > 0.01f))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        if ((Time.time > nextFire) && Input.GetButtonDown("Fire" + playerNumber + "1"))
        {
            Shoot();
            nextFire = Time.time + fireDelay;
        }

        if ((Time.time > nextShockwave) && Input.GetButtonDown("Fire" + playerNumber + "2")) {
            Shockwave();
            nextShockwave = Time.time + shockwaveDelay;
        }

        if ((Time.time > nextForcefield) && Input.GetButtonDown("Fire" + playerNumber + "3")) {
            Forcefield();
            nextForcefield = Time.time + forcefieldDelay;
        }
        if (Time.time > forcefieldDestroy) {
            Destroy(forcefield);
        }
    }

    void Shoot()
    {
        int rotation = 180;
        if (playerNumber == 2) rotation = 0;
        Instantiate(bulletPrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    void Shockwave () {
        int rotation = 180;
        if (playerNumber == 2) rotation = 0;
        Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    }

    void Forcefield () {
        forcefield = Instantiate(forcefieldPrefab, transform);
        forcefieldDestroy = Time.time + forcefieldTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("On trigger enter " + col.gameObject.tag);
        if (Time.time <= forcefieldDestroy) return;
        if (col.gameObject.tag == "Goal")
        {
            GameManager.instance.Win(playerNumber);
        }
        else if (col.gameObject.tag == "Monster")
        {
            // Destroy(col.gameObject);
            transform.position = new Vector2(
                transform.position.x * 1.05f,
                transform.position.y
            );
        }
        if (col.gameObject.tag == "Bullet") {
            transform.position = new Vector2(
                transform.position.x * 1.05f,
                transform.position.y
            );
            Destroy(col.gameObject);
        }
    }
}
