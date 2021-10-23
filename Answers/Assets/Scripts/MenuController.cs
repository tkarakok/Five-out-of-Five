using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    GameObject startPanel, choosePanel, settingsPanel, gameOverPanel, finishScreenPanel;

    public void StartButtonFunction()
    {
        startPanel.SetActive(false);
        choosePanel.SetActive(true);
    }
    public void SettingsButtonFunction()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void SettingsBackButton()
    {
        settingsPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void QuitButtonFunction()
    {
        Application.Quit();
    }
    public void Restart()
    {
        gameOverPanel.SetActive(false);
        finishScreenPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
