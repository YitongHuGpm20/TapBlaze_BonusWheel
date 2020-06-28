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
    }

    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
    }
    public void ToggleInfo()
    {
        info.SetActive(!info.activeSelf);
    }
    public void ShareButton()
    {
        socialMedia.SetActive(!socialMedia.activeSelf);
    }

    public void ToggleBGM()
    {
        GameManager.bgmOn = !GameManager.bgmOn;
    }

    public void ToggleSFX()
    {
        GameManager.sfxOn = !GameManager.sfxOn;
    }
}
