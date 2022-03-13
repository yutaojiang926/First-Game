using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    // loads and unloads the pause menu
    public void PauseButton()
    {
        // resumes game
        if (SceneManager.GetSceneByName("Pause Menu").isLoaded == true)
        {
            SceneManager.UnloadSceneAsync("Pause Menu");
            Time.timeScale = 1f;
            gamePaused = false;
        }
        // pauses game
        else
        {
            SceneManager.LoadSceneAsync("Pause Menu", LoadSceneMode.Additive);
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

}
