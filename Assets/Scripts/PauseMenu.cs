using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    // canvas with all pause menu elements
    public GameObject pauseMenuUI;

    // the text (numbers) of the volume
    public Text MusicVolumeText;
    public Text EffectsVolumeText;

    // the mixer allowing us to control volume (with exposed variables)
    public AudioMixer Mixer;

    private float MusicVolume = 1.0f;
    private float EffectsVolume = 1.0f;

    // increases music volume
    public void IncreaseMusicVolume()
    {
        // check if volume is maxed
        if(MusicVolume < 1f)
        {
            // increment volume by 0.1, set the text on a scale of 1-10
            // then set the exposed variable "BGM Volume" using a log scale to be (0 at 10) and (-80 at 0)
            MusicVolume += 0.1f;
            MusicVolumeText.text = (MusicVolume * 10).ToString("0");
            Mixer.SetFloat("BGM Volume", Mathf.Log10(MusicVolume)*20);
            
        }
    }

    public void DecreaseMusicVolume()
    {
        if (MusicVolume > 0f)
        {
            MusicVolume -= 0.1f;
            MusicVolumeText.text = (MusicVolume * 10).ToString("0");
            Mixer.SetFloat("BGM Volume", Mathf.Log10(MusicVolume) * 20);
        }
    }

    public void IncreaseEffectsVolume()
    {
        if (EffectsVolume < 1f)
        {
            EffectsVolume += 0.1f;
            EffectsVolumeText.text = (EffectsVolume * 10).ToString("0");
            Mixer.SetFloat("Effects Volume", Mathf.Log10(EffectsVolume) * 20);

        }
    }

    public void DecreaseEffectsVolume()
    {
        if (EffectsVolume > 0f)
        {
            EffectsVolume -= 0.1f;
            EffectsVolumeText.text = (EffectsVolume * 10).ToString("0");
            Mixer.SetFloat("Effects Volume", Mathf.Log10(EffectsVolume) * 20);
        }
    }



    // quits game
    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        //Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void PauseButton()
    {
        if (gamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // resumes game and disables pause screen
    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    
    // shows the pause screen, pauses the game
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;

        // set the effects and music volume texts to proper values when starting a scene
        // so that whenever we pause we are looking at the correct values
        EffectsVolumeText.text = (EffectsVolume * 10).ToString("0");
        MusicVolumeText.text = (MusicVolume * 10).ToString("0");
    }
}
