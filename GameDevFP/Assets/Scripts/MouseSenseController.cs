using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseSenseController : MonoBehaviour
{
    public static float playerMouseSensitivity;
    public Slider mouseSenseSlider;

    void Start()
    {
        playerMouseSensitivity = 50;
    }

    // Update is called once per frame
    void Update()
    {
        playerMouseSensitivity = mouseSenseSlider.value;
    }
}
