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

    public bool paused = true;

    public bool leftCameraReady = false;
    public bool rightCameraReady = false;

    public int loadCount = 1;

    public int enemyCount = 14;

    public bool moveCameras;

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

    void Update()
    {
        if (Input.anyKeyDown && paused)
        {
            StartCoroutine(StartGame());
        }
    }

    //Initializes the game for each level.
    void InitGame()
    {
        player1Origin = player1.transform.position;
        player2Origin = player2.transform.position;
    }
    IEnumerator StartGame()
    {
        GameObject.Find("MiddlePillar").GetComponent<Animator>().SetBool("gameStart", true);

        // Never do it like this, horrible.
        GameObject logo = GameObject.Find("GameLogo");
        GameObject st = GameObject.Find("StartText");
        GameObject bg = GameObject.Find("DarkenBG");
        if (logo) logo.SetActive(false);
        if (st) st.SetActive(false);
        if (bg) bg.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
        moveCameras = true;
        paused = false;
    }

    void ResetPlayers()
    {
        player1.transform.position = player1Origin;
        player2.transform.position = player2Origin;
    }

    public void Win(int winner)
    {
        // AVoid double scoring
        if (paused)
        {
            return;
        }
        // ResetPlayers();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        paused = true;
        loadCount++;
        if (winner == 1) this.player1score++;
        else this.player2score++;

    }
}
