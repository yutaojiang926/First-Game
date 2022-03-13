using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    // whether or not the game is paused
    public static bool gamePaused = false;

    // the return home confirmation prompt
    public GameObject ReturnHomePrompt;
    // the quit confirmation prompt
    public GameObject QuitGamePrompt;

    // the text (numbers) of the volume
    public Text MusicVolumeText;
    public Text EffectsVolumeText;

    // the mixer allowing us to control volume (with exposed variables)
    public AudioMixer Mixer;

    private int MusicVolume = 10;
    private int EffectsVolume = 10;

    private float[] VolumeArray = { -80, -20, -14, -10, -8, -6, -4, -3, -2, -1, 0 };

    // load the volumes on start
    private void Start()
    {
        for(int i = 0; i < 11; i++)
        {
            float temp;
            Mixer.GetFloat("BGM Volume", out temp);
            if (temp == VolumeArray[i]){
                MusicVolume = i;
                MusicVolumeText.text = MusicVolume.ToString("0");
            }
            Mixer.GetFloat("Effects Volume", out temp);
            if (temp == VolumeArray[i])
            {
                EffectsVolume = i;
                EffectsVolumeText.text = EffectsVolume.ToString("0");
            }
        }
    }
    // increases music volume
    public void IncreaseMusicVolume()
    {
        // check if volume is maxed
        if (MusicVolume < 10)
        {
            // increment volume by 1
            // then set the exposed variable "BGM Volume" using a log scale to be (0 at 10) and (-80 at 0)
            MusicVolume += 1;
            MusicVolumeText.text = MusicVolume.ToString("0");
            Mixer.SetFloat("BGM Volume", VolumeArray[MusicVolume]);

        }
    }

    public void DecreaseMusicVolume()
    {
        if (MusicVolume > 0)
        {
            MusicVolume -= 1;
            MusicVolumeText.text = MusicVolume.ToString("0");
            Mixer.SetFloat("BGM Volume", VolumeArray[MusicVolume]);
        }
    }

    public void IncreaseEffectsVolume()
    {
        if (EffectsVolume < 10)
        {
            EffectsVolume += 1;
            EffectsVolumeText.text = EffectsVolume.ToString("0");
            Mixer.SetFloat("Effects Volume", VolumeArray[EffectsVolume]);

        }
    }

    public void DecreaseEffectsVolume()
    {
        if (EffectsVolume > 0)
        {
            EffectsVolume -= 1;
            EffectsVolumeText.text = EffectsVolume.ToString("0");
            Mixer.SetFloat("Effects Volume", VolumeArray[EffectsVolume]);
        }
    }

    public void QuitPrompt()
    {
        if (QuitGamePrompt.activeSelf)
        {
            QuitGamePrompt.SetActive(false);
        }
        else
        {
            QuitGamePrompt.SetActive(true);
        }
    }

    // quits game
    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }

    // brings up or removes home prompt
    public void HomePrompt()
    {
        if (ReturnHomePrompt.activeSelf)
        {
            ReturnHomePrompt.SetActive(false);
        }
        else
        {
            ReturnHomePrompt.SetActive(true);
        }
    }

    // returns to home screen
    public void ReturnHome()
    {
        HomePrompt();
        SceneManager.LoadScene("Start");
    }
}
