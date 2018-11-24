using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance = null;
    public static AudioClip shoot1, shoot2, shoot3, shockwave, music, enemyHit, enemyDie;
    static AudioSource audioSrc;
    // Use this for initialization
    void Start()
    {
        shoot1 = Resources.Load<AudioClip>("GunShot1");
        shoot2 = Resources.Load<AudioClip>("GunShot2");
        shoot3 = Resources.Load<AudioClip>("GunShot3");
        shockwave = Resources.Load<AudioClip>("shockwave");
        enemyHit = Resources.Load<AudioClip>("EnemyCollision");
        enemyDie = Resources.Load<AudioClip>("EnemyDie");

        music = Resources.Load<AudioClip>("music");

        audioSrc = GetComponent<AudioSource>();

        audioSrc.PlayOneShot(music, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shoot":
                int num = Random.Range(0, 3);
                AudioClip[] shoots = { shoot1, shoot2, shoot3 };
                audioSrc.PlayOneShot(shoots[num], 0.25f);
                break;

            case "shockwave":
                audioSrc.PlayOneShot(shockwave, 2f);
                break;

            case "enemyHit":
                audioSrc.PlayOneShot(enemyHit, 0.5f);
                break;
            case "enemyDie":
                audioSrc.PlayOneShot(enemyDie, 1.5f);
                break;

        }
    }
}
