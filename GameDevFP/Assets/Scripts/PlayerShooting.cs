using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public delegate void ShootDelegate(bool isManual);
    public static event ShootDelegate shootAction;
    public static Action reloadAction;
    public static Action releaseAction;

    Camera playerCamera;
    public float scopedFOV = 30f;
    float defaultFOV;
    public float zoomDuration = 0.5f;

    private Coroutine zoomCoroutine;
    private TimeController timeController;

    private void Start()
    {
        playerCamera = gameObject.GetComponent<Camera>();
        defaultFOV = playerCamera.fieldOfView;
        timeController = FindAnyObjectByType<TimeController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootAction?.Invoke(true);
            timeController.Shoot();
        }

        if (Input.GetButton("Fire1")) {
            shootAction?.Invoke(false);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            releaseAction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadAction?.Invoke();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (zoomCoroutine == null)
            {
                zoomCoroutine = StartCoroutine(ZoomCoroutine(scopedFOV));
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
                zoomCoroutine = null;
            }

            StartCoroutine(ZoomCoroutine(defaultFOV));
        }
    }

    IEnumerator ZoomCoroutine(float targetFOV)
    {
        float initialFOV = playerCamera.fieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            playerCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
    }
}
