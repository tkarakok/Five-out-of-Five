using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string question;
    //public Sprite sprite;
    public string aChoiceText, bChoiceText, cChoiceText, dChoiceText;
    public Choice trueAnswer;
}

[System.Serializable]
public enum Choice
{
    aChoice,
    bChoice,
    cChoice,
    dChoice,

}



