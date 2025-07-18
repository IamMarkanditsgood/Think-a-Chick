using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : BasicScreen
{
    public Button info;
    public Button music;
    public Button sound;
    public Button vibration;

    public Sprite buttonOn;
    public Sprite buttonOff;

    private void Start()
    {
        info.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Info));
        music.onClick.AddListener(SwitchMusic);
        sound.onClick.AddListener(SwitchSound);
        vibration.onClick.AddListener(SwitchVibration);
    }

    private void OnDestroy()
    {
        info.onClick.RemoveAllListeners();
        music.onClick.RemoveAllListeners();
        sound.onClick.RemoveAllListeners();
        vibration.onClick.RemoveAllListeners();
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            music.image.sprite = buttonOn;
        }
        else
        {
            music.image.sprite = buttonOff;
        }
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            sound.image.sprite = buttonOn;
        }
        else
        {
            sound.image.sprite = buttonOff;
        }
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            vibration.image.sprite = buttonOn;
        }
        else
        {
            vibration.image.sprite = buttonOff;
        }
    }

    private void SwitchMusic()
    {
        if (music != null)
        {
            if (PlayerPrefs.GetInt("Music", 1) == 1)
            {
                PlayerPrefs.SetInt("Music", 0);
                music.image.sprite = buttonOff;
            }
            else
            {
                PlayerPrefs.SetInt("Music", 1);
                music.image.sprite = buttonOn;
            }
        }
        else
        {
            Debug.LogWarning("Music button is not assigned.");
        }
    }

    private void SwitchSound()
    {
        if (sound != null)
        {
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                PlayerPrefs.SetInt("Sound", 0);
                sound.image.sprite = buttonOff;
            }
            else
            {
                PlayerPrefs.SetInt("Sound", 1);
                sound.image.sprite = buttonOn;
            }
        }
        else
        {
            Debug.LogWarning("Sound button is not assigned.");
        }
    }

    private void SwitchVibration()
    {
        if (vibration != null)
        {
            if (PlayerPrefs.GetInt("Vibration", 1) == 1)
            {
                PlayerPrefs.SetInt("Vibration", 0);
                vibration.image.sprite = buttonOff;
            }
            else
            {
                PlayerPrefs.SetInt("Vibration", 1);
                vibration.image.sprite = buttonOn;
            }
        }
        else
        {
            Debug.LogWarning("Vibration button is not assigned.");
        }
    }
}
