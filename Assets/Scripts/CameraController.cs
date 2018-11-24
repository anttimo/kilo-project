using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera targetCamera;

    public bool isLeftCamera = true;
    public float cameraSpeed = 12f;
    public GameObject arena;
    public static float arenaWidth = 36f;

    public GameObject player;
    private Vector3 offset;

    private bool isReady = false;
    private bool moveCameras = false;
    void Awake()
    {
        targetCamera = GetComponent<Camera>();
        var v3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, 0, transform.position.z));
        var x = v3.x;
        if (!isLeftCamera)
        {
            x = -1 * x;
        }

        targetCamera.transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    void Start()
    {
        StartCoroutine(Wait());
    }

    public void MoveCamera()
    {
        moveCameras = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2);
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveCameras)
        {
            return;
        }

        if (GameManager.instance.paused)
        {
            var offsetX = transform.position.x - cameraSpeed / 40;

            if (!isLeftCamera)
            {
                offsetX = transform.position.x + cameraSpeed / 40;
            }

            targetCamera.transform.position = new Vector3(offsetX, transform.position.y, transform.position.z);

            if (Mathf.Abs(targetCamera.transform.position.x) >= arenaWidth)
            {
                if (isLeftCamera)
                {
                    GameManager.instance.leftCameraReady = true;
                }
                else
                {
                    GameManager.instance.rightCameraReady = true;
                }

                // Make sure both cameras have ended their panning before starting the game.
                if (GameManager.instance.leftCameraReady && GameManager.instance.rightCameraReady)
                {
                    GameManager.instance.paused = false;
                }

                offset = transform.position - player.transform.position;
            }

            return;
        }

        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
