using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //List of all the controls that are allowed in the game.
    //So the time don't move forward when pressing a non control button.
    List<KeyCode> allowedKeys;

    //The Speed the Time speed up and down.
    [SerializeField] float timeScaleAccelerationSpeed;

    bool noKey = true;

    bool isShooting = false;
    float shootTimeCounter = 0f;

    void Awake()
    {
        allowedKeys = new List<KeyCode>
        { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Space, KeyCode.Mouse0, KeyCode.R };

        //Start Forzen
        Time.timeScale = 0.05f;

        //Smoothing the TimeScale
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
    void Update()
    {
        noKey = true;

        // Check if shooting time effect is active
        if (isShooting)
        {
            shootTimeCounter -= Time.unscaledDeltaTime;
            if (shootTimeCounter <= 0)
            {
                isShooting = false;
            }
        }

        if (!isShooting)
        {
            // If the player pressed any allowed key
            foreach (KeyCode key in allowedKeys)
            {
                if (Input.GetKey(key))
                {
                    noKey = false;
                    // Speed up time
                    Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, timeScaleAccelerationSpeed * Time.unscaledDeltaTime);

                    // Smoothing the TimeScale
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    break;
                }
            }

            // If no key is pressed
            if (noKey)
            {
                // Slow down time
                Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.05f, timeScaleAccelerationSpeed * Time.unscaledDeltaTime);
                Debug.Log("scale: " + Time.timeScale);
                Debug.Log("unscaled: " + Time.unscaledDeltaTime);
                // Smoothing the TimeScale
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
        else
        {
            // Speed up time due to shooting
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    public void Shoot()
    {
        isShooting = true;
        shootTimeCounter = 0.5f;
    }
}