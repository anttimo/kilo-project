using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartMenu : MonoBehaviour {

    void Awake()
    {
        GameManager.instance.SetStartMenu(gameObject);
    }
}
