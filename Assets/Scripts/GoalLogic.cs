using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLogic : MonoBehaviour
{

    public ParticleSystem winParticleSystem;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("On trigger enter 2D");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("On trigger enter 2D was player");
            winParticleSystem.Play();
        }
    }
}
