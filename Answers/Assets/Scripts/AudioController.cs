using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    bool muted = false;
    [SerializeField] Sprite onVoiceIcon,offVoiceIcon;
    [SerializeField] Button audioButton;
    

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            loadSettings() ;
        }
        updateIcon();
        AudioListener.pause = muted;
    }
    
    private void updateIcon()
    {
        if (muted == false)
        {
            audioButton.GetComponent<Image>().sprite = onVoiceIcon;
        }
        else
        {
            audioButton.GetComponent<Image>().sprite = offVoiceIcon;
        }
    }

    public void audioController()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        saveSettings();
        updateIcon();
    }
    private void loadSettings()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void saveSettings()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}