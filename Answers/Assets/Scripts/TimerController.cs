using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    [SerializeField] Text secText;
    [SerializeField] GameObject gameOverPanel;
    public static float sec = 21;

    public void StartCounter()
    {
       StopAllCoroutines();
       sec = 21;
       StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (sec > 0)
        {
            yield return new WaitForSeconds(1);
            sec--;
            secText.text = sec.ToString();
        }
        gameOverPanel.SetActive(true);
    }


}
