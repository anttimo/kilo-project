using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public GameObject player1;
    public GameObject player2;
    
    private int level = 1;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {

    }

    public static void Win  (int winner) {
        Debug.Log(winner);
        // TODO: Reset player locations to original places
    }
}
