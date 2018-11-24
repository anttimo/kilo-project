using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public GameObject player1;
    public GameObject player2;

    static Vector3 player1Origin;
    static Vector3 player2Origin;

    public bool paused = true;

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
        player1Origin = player1.transform.position;
        player2Origin = player2.transform.position;
    }

    void ResetPlayers()
    {
        player1.transform.position = player1Origin;
        player2.transform.position = player2Origin;
    }

    public void Win(int winner)
    {
        ResetPlayers();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
