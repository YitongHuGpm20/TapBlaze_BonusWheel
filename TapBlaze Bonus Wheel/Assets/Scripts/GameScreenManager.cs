using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenManager : MonoBehaviour
{
    public GameObject options;
    public GameObject optionsOnButton;
    public GameObject optionsOffButton;
    public GameObject custom;
    public GameObject report;
    public GameObject autoSpinOptions;
    public GameObject settings;
    public GameObject win;
    public GameObject prizeIcon;

    [HideInInspector]
    public GameObject[] wheelGame;

    private void Start()
    {
        wheelGame = GameObject.FindGameObjectsWithTag("WheelGame");
    }

    public void ToggleOptions()
    {
        options.SetActive(!options.activeSelf);
        optionsOffButton.SetActive(!optionsOffButton.activeSelf);
        optionsOnButton.SetActive(!optionsOnButton.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleCustom()
    {
        custom.SetActive(!custom.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleReport()
    {
        report.SetActive(!report.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleAutoSpinOptions()
    {
        autoSpinOptions.SetActive(!autoSpinOptions.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
        SoundManager.instance.PlaySound(0);
    }

    public void ClaimButton()
    {
        foreach (GameObject w in wheelGame)
            w.gameObject.SetActive(true);
        win.SetActive(false);
        prizeIcon.SetActive(false);
        SoundManager.instance.PlaySound(0);
        SoundManager.instance.StopSound(2);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
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
