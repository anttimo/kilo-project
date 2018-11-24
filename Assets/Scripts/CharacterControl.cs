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
  public Rigidbody2D rb;
  public bool knockingBack = false;

  private float maxY;

  public float forcefieldDestroy;

  public float forcefieldTime = 2f;

  public float nextForcefield;

  public float forcefieldDelay = 8f;

  public AudioSource shoot;

  // Use this for initialization

  void Awake()
  {
    maxY = Camera.main.orthographicSize * 2.0f;
  }

  void Start()
  {
    originLocation = transform.position;
    fireDelay = 0.5f;
    nextFire = 0f;
    nextShockwave = 0f;
    forcefieldDestroy = 0f;
    nextForcefield = 0f;
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

    var yPosition = Mathf.Clamp(transform.position.y, -1 * maxY / 2, maxY / 2);
    transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

    if (moveX != 0 && !spriteRenderer.flipX ? (moveX < 0.01f) : (moveX > 0.01f))
    {
      spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    if ((Time.time > nextFire) && Input.GetButtonDown("Fire" + playerNumber + "1"))
    {
      Shoot();
      nextFire = Time.time + fireDelay;
    }

    if ((Time.time > nextShockwave) && Input.GetButtonDown("Fire" + playerNumber + "2"))
    {
      Shockwave();
      nextShockwave = Time.time + shockwaveDelay;
    }

    if ((Time.time > nextForcefield) && Input.GetButtonDown("Fire" + playerNumber + "3"))
    {
      Forcefield();
      nextForcefield = Time.time + forcefieldDelay;
    }
    if (Time.time > forcefieldDestroy)
    {
      Destroy(forcefield);
    }
  }

  void Shoot()
  {
    int rotation = 180;
    if (playerNumber == 2) rotation = 0;
    Instantiate(bulletPrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    SoundManagerScript.PlaySound("shoot");
  }

  void Shockwave()
  {
    int rotation = 180;
    if (playerNumber == 2) rotation = 0;
    Instantiate(shockwavePrefab, firepoint.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
    SoundManagerScript.PlaySound("shockwave");
  }

  void Forcefield()
  {
    forcefield = Instantiate(forcefieldPrefab, transform);
    forcefieldDestroy = Time.time + forcefieldTime;
  }

  void OnTriggerEnter2D(Collider2D col)
  {

    if (Time.time <= forcefieldDestroy) return;
    if (col.gameObject.tag == "Goal")
    {
      GameManager.instance.Win(playerNumber);
    }
    else if (col.gameObject.tag == "Monster")
    {
      StartCoroutine(Knockback());
      if (knockingBack)
      {
        Destroy(col.gameObject);
        SoundManagerScript.PlaySound("enemyHit");
      }
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
        new Vector2(transform.position.x / Mathf.Abs(transform.position.x) * 15, 0),
        ForceMode2D.Impulse
    );
    yield return new WaitForSeconds(0.25f);
    rb.velocity = Vector2.zero;
    rb.angularVelocity = 0;
    knockingBack = false;
    Debug.Log("Stop KB");
  }
}
