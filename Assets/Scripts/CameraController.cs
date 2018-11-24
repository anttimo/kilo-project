﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera targetCamera;

    public bool isLeftCamera = true;
    public float cameraSpeed = 12f;
    public GameObject arena;
    private float arenaWidth = 20f;

    public GameObject player;
    private Vector3 offset;

    private bool isReady = false;
    private bool moveCameras = false;
    void Awake()
    {
        Debug.Log(arenaWidth);
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

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2);
        moveCameras = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveCameras)
        {
            return;
        }

        if (!isReady)
        {
            var offsetX = transform.position.x + Mathf.Sign(transform.position.x) * cameraSpeed / 40;
            targetCamera.transform.position = new Vector3(offsetX, transform.position.y, transform.position.z);

            if (Mathf.Abs(targetCamera.transform.position.x) >= arenaWidth)
            {
                isReady = true;
                offset = transform.position - player.transform.position;
            }
            return;
        }

        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
