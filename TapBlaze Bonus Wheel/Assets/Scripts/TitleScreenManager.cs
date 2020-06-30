using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject settings;
    public GameObject info;
    public GameObject socialMedia;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
        SoundManager.instance.PlaySound(0);
    }
    public void ToggleInfo()
    {
        info.SetActive(!info.activeSelf);
        SoundManager.instance.PlaySound(0);
    }
    public void ShareButton()
    {
        socialMedia.SetActive(!socialMedia.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleBGM()
    {
        GameManager.bgmOn = !GameManager.bgmOn;
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleSFX()
    {
        GameManager.sfxOn = !GameManager.sfxOn;
        SoundManager.instance.PlaySound(0);
    }
}
