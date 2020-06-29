﻿using System.Collections;
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

    public void ToggleOptions()
    {
        options.SetActive(!options.activeSelf);
        optionsOffButton.SetActive(!optionsOffButton.activeSelf);
        optionsOnButton.SetActive(!optionsOnButton.activeSelf);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleCustom()
    {
        custom.SetActive(!custom.activeSelf);
    }

    public void ToggleReport()
    {
        report.SetActive(!report.activeSelf);
    }

    public void ToggleAutoSpinOptions()
    {
        autoSpinOptions.SetActive(!autoSpinOptions.activeSelf);
    }

    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
    }
}
