using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField] Text questionText, choiceA, choiceB, choiceC, choiceD;
    [SerializeField] Button nextQuestionButton;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject questionPanel, trueAnswerPanel, wrongAnswerPanel, choosePanel;
    Color color;
    QuestionList questionList;
    Question question;
    IEnumerator coroutine;

    private void Start()
    {
        color = buttons[0].gameObject.GetComponent<Image>().color;
        questionList = GetComponent<QuestionList>();
    }


    public void GiveQuestion(int categoryID)
    {
        choosePanel.SetActive(false);
        questionPanel.SetActive(true);
        ButtonRefresh();
        trueAnswerPanel.SetActive(false);

        if (categoryID == 1)
        {
            question = questionList.Culture[Random.Range(0, questionList.Culture.Count)];
            PrintQuestion();
        }
        else if (categoryID == 2)
        {
            question = questionList.Math[Random.Range(0, questionList.Math.Count)];
            PrintQuestion();
        }
        else if (categoryID == 3)
        {
            question = questionList.Geography[Random.Range(0, questionList.Geography.Count)];
            PrintQuestion();
        }
        else if (categoryID == 4)
        {
            question = questionList.Science[Random.Range(0, questionList.Science.Count)];
            PrintQuestion();
        }
        else if (categoryID == 5)
        {
            question = questionList.Sport[Random.Range(0, questionList.Sport.Count)];
            PrintQuestion();
        }
        else if (categoryID == 6)
        {
            question = questionList.Cinema[Random.Range(0, questionList.Cinema.Count)];
            PrintQuestion();
        }
    }

    void PrintQuestion()
    {
        questionText.text = question.question;
        choiceA.text = question.aChoiceText;
        choiceB.text = question.bChoiceText;
        choiceC.text = question.cChoiceText;
        choiceD.text = question.dChoiceText;
    }

    public void TakeAnswer(int choiceID)
    {
        Choice _choice;
        Button _selectedButton;
        if (choiceID == 1)
        {
            _choice = Choice.aChoice;
            _selectedButton = buttons[0];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 2)
        {
            _choice = Choice.bChoice;
            _selectedButton = buttons[1];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 3)
        {
            _choice = Choice.cChoice;
            _selectedButton = buttons[2];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 4)
        {
            _choice = Choice.dChoice;
            _selectedButton = buttons[3];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }

    }
    void ButtonRefresh()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponent<Image>().color = color;
            buttons[i].interactable = true;
        }
       
    }

    public void NewChoose()
    {
        trueAnswerPanel.SetActive(false);
        questionPanel.SetActive(false);
        choosePanel.SetActive(true);
    }

    IEnumerator AnswerControl(Choice choice, Button selectedButton)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        selectedButton.gameObject.GetComponent<Image>().color = Color.blue;
        yield return new WaitForSeconds(1);

        if (choice == question.trueAnswer)
        {
            selectedButton.gameObject.GetComponent<Image>().color = Color.green;
            trueAnswerPanel.SetActive(true);
            nextQuestionButton.interactable = true;
        }
        else
        {
            selectedButton.gameObject.GetComponent<Image>().color = Color.red;
            wrongAnswerPanel.SetActive(true );
            yield return new WaitForSeconds(1);
            wrongAnswerPanel.SetActive(false);
            selectedButton.gameObject.GetComponent<Image>().color = color;

            ButtonRefresh();
        }

    }
}
