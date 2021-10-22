using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField] Text questionText, choiceA, choiceB, choiceC, choiceD, pointText, finishCategoryText, scoreText, healthText, refreshJokerText,timerJokerText;
    [SerializeField] Button nextQuestionButton, refreshJokerButton,timerJokerButton;
    [SerializeField] Button[] choiceButtons;
    [SerializeField] Button[] categoryButtons;
    [SerializeField] GameObject questionPanel, trueAnswerPanel, wrongAnswerPanel, choosePanel, startPanel, welldonePanel;
    [SerializeField] int asd,timerJoker,point, turn, choiceNumber, finishCategory, health, refreshJoker;
    Color color;
    QuestionList questionList;
    Question question;
    IEnumerator coroutine;
    [SerializeField] TimerController timerController;
    private void Start()
    {
        UiRefresh();
        color = choiceButtons[0].gameObject.GetComponent<Image>().color;
        questionList = GetComponent<QuestionList>();
    }


    public void GiveQuestion(int categoryID)
    {
        timerController.StartCounter();
        choosePanel.SetActive(false);
        questionPanel.SetActive(true);
        ButtonRefresh();
        trueAnswerPanel.SetActive(false);

        if (categoryID == 1)
        {
            choiceNumber = 1;
            question = questionList.Culture[Random.Range(0, questionList.Culture.Count)];
            PrintQuestion();
            categoryButtons[0].interactable = false;
        }
        else if (categoryID == 2)
        {
            choiceNumber = 2;
            question = questionList.Math[Random.Range(0, questionList.Math.Count)];
            PrintQuestion();
            categoryButtons[1].interactable = false;
        }
        else if (categoryID == 3)
        {
            choiceNumber = 3;
            question = questionList.Geography[Random.Range(0, questionList.Geography.Count)];
            PrintQuestion();
            categoryButtons[2].interactable = false;
        }
        else if (categoryID == 4)
        {
            choiceNumber = 4;
            question = questionList.Science[Random.Range(0, questionList.Science.Count)];
            PrintQuestion();
            categoryButtons[3].interactable = false;
        }
        else if (categoryID == 5)
        {
            choiceNumber = 5;
            question = questionList.Sport[Random.Range(0, questionList.Sport.Count)];
            PrintQuestion();
            categoryButtons[2].interactable = false;
        }
        else if (categoryID == 6)
        {
            choiceNumber = 6;
            question = questionList.Cinema[Random.Range(0, questionList.Cinema.Count)];
            PrintQuestion();
            categoryButtons[5].interactable = false;
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
            _selectedButton = choiceButtons[0];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 2)
        {
            _choice = Choice.bChoice;
            _selectedButton = choiceButtons[1];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 3)
        {
            _choice = Choice.cChoice;
            _selectedButton = choiceButtons[2];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }
        else if (choiceID == 4)
        {
            _choice = Choice.dChoice;
            _selectedButton = choiceButtons[3];
            coroutine = AnswerControl(_choice, _selectedButton);
            StartCoroutine(coroutine);
        }

    }
    void ButtonRefresh()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.GetComponent<Image>().color = color;
            choiceButtons[i].interactable = true;
        }
    }

    public void NewChoose()
    {
        trueAnswerPanel.SetActive(false);
        GiveQuestion(choiceNumber);
    }
    public void UiRefresh()
    {
        scoreText.text = point.ToString();
        finishCategoryText.text = finishCategory.ToString();
        refreshJokerText.text = refreshJoker.ToString();
        timerJokerText.text = timerJoker.ToString();
    }
  
    public void PointRefresh()
    {
        pointText.text = point.ToString();
    }
    IEnumerator Welldone()
    {
        welldonePanel.SetActive(true);
        yield return new WaitForSeconds(2);
        welldonePanel.SetActive(false);

    }

    public void RefreshQuestionJoker()
    {
        if (refreshJoker != 0)
        {
            refreshJoker--;
            refreshJokerText.text = refreshJoker.ToString();
            GiveQuestion(choiceNumber);
            if (refreshJoker == 0)
            {
                refreshJokerButton.interactable = false;
            }
        }
    }

    public void TimerJoker(){
        if (timerJoker != 0)
        {
            timerJoker--;
            timerJokerText.text = timerJoker.ToString();
            TimerController.sec += 5;
            if (timerJoker == 0)
            {
                timerJokerButton.interactable = false;
            }
        }
    }

    IEnumerator AnswerControl(Choice choice, Button selectedButton)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].interactable = false;
        }
        selectedButton.gameObject.GetComponent<Image>().color = Color.blue;
        yield return new WaitForSeconds(1);

        if (choice == question.trueAnswer)
        {
            point += 10;
            if (turn == 4)
            {
                finishCategory++;
                // game finish
                choiceNumber = 0;
                questionPanel.SetActive(false);
                StartCoroutine(Welldone());
                choosePanel.SetActive(true);
                turn = 0;
                UiRefresh();
            }
            else
            {
                turn++;
                trueAnswerPanel.SetActive(true);
            }
            PointRefresh();
            selectedButton.gameObject.GetComponent<Image>().color = Color.green;
            nextQuestionButton.interactable = true;
        }
        else
        {
            selectedButton.gameObject.GetComponent<Image>().color = Color.red;
            wrongAnswerPanel.SetActive(true);
            yield return new WaitForSeconds(1);
            wrongAnswerPanel.SetActive(false);
            point -= 5;
            health--;
            healthText.text = health.ToString();
            if (health == 0)
            {
                //game over panel
            }
            PointRefresh();
            selectedButton.gameObject.GetComponent<Image>().color = color;
            ButtonRefresh();
        }

    }
}
