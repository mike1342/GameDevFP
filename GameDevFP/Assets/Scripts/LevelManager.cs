using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public string nextScene;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelLost() {
        isGameOver = true;
        Debug.Log("Level Lost");
        Invoke("LoadCurrLevel", 2);
    }

    public void LevelBeat() {
        isGameOver = true;
        Debug.Log("Level Won");
        Invoke("LoadNextLevel", 2);
    }
    
    void LoadCurrLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextLevel() {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (MouseSenseController.playerMouseSensitivity == 0)
            {
                MouseLook.mouseSensitivity = 10;
            }
            else
            {
                MouseLook.mouseSensitivity = MouseSenseController.playerMouseSensitivity;
            }
        }
        if(string.IsNullOrEmpty(nextScene)) {
            LoadCurrLevel();
        } else {
            SceneManager.LoadScene(nextScene);
        }
    }
}