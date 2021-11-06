using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField] Text questionText, choiceA, choiceB, choiceC, choiceD, pointText, finishCategoryText, scoreText, healthText, refreshJokerText, timerJokerText, chooseHealthText, chooseTimerText, chooseRefreshText, correctQuestionText, bestScoreText, finishScoreText;
    [SerializeField] Button nextQuestionButton, refreshJokerButton, timerJokerButton;
    [SerializeField] Button[] choiceButtons;
    [SerializeField] Button[] categoryButtons;
    [SerializeField] GameObject questionPanel, trueAnswerPanel, wrongAnswerPanel, choosePanel, startPanel, gameOverPanel, welldonePanel, finishScreenPanel;
    [SerializeField] int correctQuestion, timerJoker, point, turn, choiceNumber, finishCategory, refreshJoker;
    
    public static int health = 3;
    Color color;
    QuestionList questionList;
    Question question;
    IEnumerator coroutine;
    [SerializeField] TimerController timerController;
    [SerializeField] AdsController adsController;
    [SerializeField] AudioClip trueAnswerClip, wrongAnswerClip;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        UiRefreshOnQuestionPanel();
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
            int random = Random.Range(0, questionList.Culture.Count);
            question = questionList.Culture[random];
            questionList.Culture.Remove(question);
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
            categoryButtons[4].interactable = false;
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

    public void UiRefreshOnQuestionPanel()
    {
        pointText.text = point.ToString();
        refreshJokerText.text = refreshJoker.ToString();
        timerJokerText.text = timerJoker.ToString();
        healthText.text = health.ToString();
    }

    public void UiRefreshOnChoosePanel()
    {
        scoreText.text = point.ToString();
        finishCategoryText.text = finishCategory.ToString();
        chooseHealthText.text = health.ToString();
        chooseRefreshText.text = refreshJoker.ToString();
        chooseTimerText.text = timerJoker.ToString();
    }

    IEnumerator Welldone()
    {
        UiRefreshOnChoosePanel();
        welldonePanel.SetActive(true);
        yield return new WaitForSeconds(2);
        welldonePanel.SetActive(false);
        correctQuestion = 0;
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

    public void TimerJoker()
    {
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

    public void PointChecker()
    {
        if (TimerController.sec > 15)
        {
            point += 10;
        }
        else if (TimerController.sec > 10 && TimerController.sec <= 15)
        {
            point += 7;
        }
        else if (TimerController.sec > 5 && TimerController.sec <= 10)
        {
            point += 5;
        }
        else
        {
            point += 3;
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
            audioSource.PlayOneShot(trueAnswerClip);
            correctQuestion++;
            correctQuestionText.text = correctQuestion.ToString();
            PointChecker();
            if (turn == 4)
            {
                adsController.ResetAdsCounter();
                finishCategory++;
                choiceNumber = 0;
                questionPanel.SetActive(false);
                StartCoroutine(Welldone());
                if (finishCategory == 6)
                {
                    if (point > PlayerPrefs.GetInt("bestscore"))
                    {
                        PlayerPrefs.SetInt("bestscore", point);
                    }
                    FinishScreenController();
                }
                choosePanel.SetActive(true);
                turn = 0;
                UiRefreshOnQuestionPanel();
            }
            else
            {
                turn++;
                trueAnswerPanel.SetActive(true);
            }
            UiRefreshOnQuestionPanel();
            selectedButton.gameObject.GetComponent<Image>().color = Color.green;
            nextQuestionButton.interactable = true;
        }
        else
        {
            audioSource.PlayOneShot(wrongAnswerClip);
            selectedButton.gameObject.GetComponent<Image>().color = Color.red;
            wrongAnswerPanel.SetActive(true);
            yield return new WaitForSeconds(1);
            wrongAnswerPanel.SetActive(false);
            point -= 5;
            health--;
            healthText.text = health.ToString();
            if (health == 0)
            {
                GameOver();
            }
            UiRefreshOnQuestionPanel();
            selectedButton.gameObject.GetComponent<Image>().color = color;
            ButtonRefresh();
        }

    }

    public void GameOver()
    {
        questionPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        foreach (var item in categoryButtons)
        {
            item.interactable = true;
            item.GetComponent<Image>().color = color;
        }
        health = 3;
        refreshJoker = 3;
        timerJoker = 3;
        point = 0;
        finishCategory = 0;
        UiRefreshOnChoosePanel();
        UiRefreshOnQuestionPanel();
    }

    public void FinishScreenController()
    {
        questionPanel.SetActive(false);
        finishScreenPanel.SetActive(true);
        int bestScore = PlayerPrefs.GetInt("bestscore");
        bestScoreText.text = bestScore.ToString();
        finishScoreText.text = point.ToString();
    }
}
