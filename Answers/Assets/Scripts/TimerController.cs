using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    [SerializeField] Text secText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip lastSeconds;
    [SerializeField] QuestionController questionController;
    public static float sec = 20;
    int counter = 0;

    public void StartCounter()
    {
       StopAllCoroutines();
       sec = 20;
       StartCoroutine(Timer());
    }
    void LastSecondsController(){
        if (sec < 5)
        {
             audioSource.PlayOneShot(lastSeconds);
        }
    }
    IEnumerator Timer()
    {
        while (sec > 0)
        {
            yield return new WaitForSeconds(1);
            sec--;
            secText.text = sec.ToString();
            if (sec < 5)
            {
                counter++;
                if (counter == 1)
                {
                    LastSecondsController();
                }
            }
        }
        
        questionController.GameOver();
    }


}
