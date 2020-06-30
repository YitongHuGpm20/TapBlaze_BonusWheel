using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxelBusters;
using VoxelBusters.NativePlugins;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject settings;
    public GameObject info;
    public GameObject socialMedia;
    public Sprite[] soundIcons;
    public Image bgmButton;
    public Image sfxButton;

    private void Start()
    {
        if (GameManager.bgmOn)
            bgmButton.sprite = soundIcons[0];
        else
            bgmButton.sprite = soundIcons[1];
        if (GameManager.sfxOn)
            sfxButton.sprite = soundIcons[2];
        else
            sfxButton.sprite = soundIcons[3];
    }

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
        if (GameManager.bgmOn)
            bgmButton.sprite = soundIcons[0];
        else
            bgmButton.sprite = soundIcons[1];
    }

    public void ToggleSFX()
    {
        GameManager.sfxOn = !GameManager.sfxOn;
        SoundManager.instance.PlaySound(0);
        if (GameManager.sfxOn)
            sfxButton.sprite = soundIcons[2];
        else
            sfxButton.sprite = soundIcons[3];
    }

    private void ShareSheet()
    {
        //ShareSheet shareSheet = new ShareSheet();
        //shareSheet.Text = ""
    }
}
