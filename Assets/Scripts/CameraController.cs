using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera targetCamera;

    void Awake()
    {
        targetCamera = GetComponent<Camera>();

        var v3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, 0, 0));
        Debug.Log(v3);
        targetCamera.transform.position = new Vector3(v3.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
