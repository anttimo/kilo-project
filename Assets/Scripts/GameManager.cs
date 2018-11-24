using UnityEngine;
using UnityEngine.UI;
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

    public int player1score = 0;

    public int player2score = 0;

    public Text player1scoreText;
    public Text player2scoreText;

    public bool paused = true;

    public int loadCount = 1;

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
        player1scoreText.text = "0";
        player2scoreText.text = "0";
    }

    void ResetPlayers()
    {
        player1.transform.position = player1Origin;
        player2.transform.position = player2Origin;
    }

    public void Win(int winner)
    {
        // ResetPlayers();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        paused = true;
        loadCount++;
        if (winner == 1) this.player1score++;
        else this.player2score++;

        player1scoreText.text = player1score.ToString();
        player2scoreText.text = player2score.ToString();

    }
}
