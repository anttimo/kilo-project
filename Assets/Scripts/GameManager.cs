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

    private bool readyForNewGame = true;

    public GameObject startMenu;


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        InitGame();
    }

    void Update()
    {
        if (!readyForNewGame) {
            return;
        }

        if (Input.anyKeyDown && paused)
        {
            StartCoroutine(StartGame());
        }
    }

    public void SetStartMenu(GameObject menu)
    {
        startMenu = menu;
    }
    //Initializes the game for each level.
    void InitGame()
    {
        player1Origin = player1.transform.position;
        player2Origin = player2.transform.position;
        if (startMenu)
        {
            startMenu.SetActive(true);
        }
    }
    IEnumerator StartGame()
    {
        GameObject.Find("MiddlePillar").GetComponent<Animator>().SetBool("gameStart", true);
        if (startMenu) {
            startMenu.SetActive(false);
        }
        yield return new WaitForSecondsRealtime(1);
        moveCameras = true;
        paused = false;
        readyForNewGame = false;
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
        paused = true;
        if (winner == 1) this.player1score++;
        else this.player2score++;
        StartCoroutine(RunWinLogicWithWait());
    }

    IEnumerator RunWinLogicWithWait() {
        yield return new WaitForSecondsRealtime(3);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        loadCount++;
        readyForNewGame = true;
    }
}
