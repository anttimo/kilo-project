using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartMenu : MonoBehaviour {
    public static StartMenu instance = null;

    void Awake()
    {
        GameManager.instance.SetStartMenu(gameObject);
    }
}
