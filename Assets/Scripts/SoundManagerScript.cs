using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

  public static AudioClip shoot1, shoot2, shoot3, shockwave, music, enemyHit;
  static AudioSource audioSrc;
  // Use this for initialization
  void Start()
  {
    shoot1 = Resources.Load<AudioClip>("GunShot1");
    shoot2 = Resources.Load<AudioClip>("GunShot2");
    shoot3 = Resources.Load<AudioClip>("GunShot3");
    shockwave = Resources.Load<AudioClip>("shockwave");
    enemyHit = Resources.Load<AudioClip>("EnemyCollision");

    music = Resources.Load<AudioClip>("music");

    audioSrc = GetComponent<AudioSource>();

    audioSrc.PlayOneShot(music);
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
        audioSrc.PlayOneShot(shoots[num]);
        break;

      case "shockwave":
        audioSrc.PlayOneShot(shockwave);
        break;

      case "enemyHit":
        audioSrc.PlayOneShot(enemyHit);
        break;

    }
  }
}
