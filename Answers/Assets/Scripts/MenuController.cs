using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    GameObject startPanel, choosePanel, settingsPanel, gameOverPanel, finishScreenPanel;
    [SerializeField] Sprite crossIcon, settingsIcon;
    [SerializeField] Button settingsButton;
    public void StartButtonFunction()
    {
        startPanel.SetActive(false);
        choosePanel.SetActive(true);
    }
    public void SettingsButtonFunction()
    {
        settingsButton.GetComponent<Image>().sprite = crossIcon;
        settingsPanel.SetActive(true);
    }
    public void SettingsBackButton()
    {
        settingsPanel.SetActive(false);
        settingsButton.GetComponent<Image>().sprite = settingsIcon;

    }
    public void BackToMenu()
    {
        choosePanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void QuitButtonFunction()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
