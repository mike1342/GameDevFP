using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerBody;

    public static float mouseSensitivity;

    float pitch = 0;

    void Start()
    {
        mouseSensitivity = MouseSenseController.playerMouseSensitivity;
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (mouseSensitivity < 1)
        {
            mouseSensitivity = 1000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

        playerBody.Rotate(Vector3.up * moveX);

        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
