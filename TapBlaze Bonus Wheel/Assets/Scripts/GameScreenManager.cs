using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenManager : MonoBehaviour
{
    public GameObject options;
    public GameObject optionsOnButton;
    public GameObject optionsOffButton;

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
}
