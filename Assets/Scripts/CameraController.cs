using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera targetCamera;

    public bool isLeftCamera = true;
    public float cameraSpeed = 2f;
    void Awake()
    {
        targetCamera = GetComponent<Camera>();

        var v3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, 0, 0));
        var x = v3.x;
        if (!isLeftCamera)
        {
            x = -1 * x;
        }

        targetCamera.transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var offsetX = transform.position.x + Mathf.Sign(transform.position.x) * cameraSpeed / 40;
        targetCamera.transform.position = new Vector3(offsetX, transform.position.y, transform.position.z);
    }
}
