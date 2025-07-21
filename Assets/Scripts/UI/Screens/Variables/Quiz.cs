using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : BasicScreen
{
    public Button close;
    public TMP_Text timer;

    //Current question pointer
    public int currentQuestionIndex = 0;
    public Image[] pointers;
    public Sprite currentQuestionPoint;
    public Sprite defaultQuestionPoint;
    public Sprite correntQuestionPoint;
    public Sprite wrongQuestionPoint;

    //Replies
    public Button[] replyButtons;
    public TMP_Text[] replyTexts;
    public Sprite defaultReplyButtonSprite;
    public Sprite correctReplyButtonSprite;
    public Sprite wrongReplyButtonSprite;
    public Sprite currentReplyButtonSprite;

    //Question
    public Image[] emojiImages;

    //Answer
    public Button answerButton;
    //Results
    public List<bool> results;

    private Coroutine timerCoroutine;

    public GameQuizConfig[] quizConfigs;

    private GameQuizConfig currentQuiz;

    private int currentChoosedAnswerIndex = -1;

    private void Start()
    {
        close.onClick.AddListener(Close);
        answerButton.onClick.AddListener(Answer);
        for (int i = 0; i < replyButtons.Length; i++)
        {
            int index = i; // Capture the current index
            replyButtons[i].onClick.AddListener(() => ChooseAnswer(index));
        }

    }

    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
        answerButton.onClick.RemoveListener(Answer);
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].onClick.RemoveAllListeners();
        }

    }

    public override void SendData(object data)
    {
        int index = (int)data;
        currentQuiz = quizConfigs[index];
    }

    public override void ResetScreen()
    {
        StopAllCoroutines();
    }

    public override void SetScreen()
    {
        answerButton.interactable = false;
        currentQuestionIndex = 0;
        currentChoosedAnswerIndex = -1;

        for(int i = 0; i < currentQuiz.questions.Count; i++)
        {
            results[i] = false;
        }

        for (int i = 0; i < pointers.Length; i++)
        {
            pointers[i].sprite = defaultQuestionPoint;
        }

        for (int i = 0; i < emojiImages.Length; i++)
        {
            emojiImages[i].gameObject.SetActive(false);
        }


        StartGame();
    }

    private void StartGame()
    {
        timerCoroutine = StartCoroutine(Timer());
        SetQuestion();
    }

    private void StopGame()
    {
        StopAllCoroutines();
        ShowResults();
    }

    private void SetQuestion()
    {
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponent<Image>().sprite = defaultReplyButtonSprite;
        }

        if (currentQuestionIndex < currentQuiz.questions.Count)
        {
            pointers[currentQuestionIndex].sprite = currentQuestionPoint;
            for(int i = 0;i < currentQuiz.questions[currentQuestionIndex].options.Count; i++)
            {
                replyTexts[i].text = currentQuiz.questions[currentQuestionIndex].options[i];
            }

            for (int i = 0; i < currentQuiz.questions[currentQuestionIndex].questionEmoji.Count; i++)
            {
                emojiImages[i].sprite = currentQuiz.questions[currentQuestionIndex].questionEmoji[i];
                emojiImages[i].gameObject.SetActive(true);
            }
        }
        else
        {
            StopGame();
        }
    }

    private void ChooseAnswer(int index)
    {
        if (currentQuestionIndex < currentQuiz.questions.Count)
        {
            currentChoosedAnswerIndex = index;
            for (int i = 0; i < replyButtons.Length; i++)
            {
                replyButtons[i].GetComponent<Image>().sprite = defaultQuestionPoint;
            }         
            replyButtons[index].GetComponent<Image>().sprite = currentReplyButtonSprite;

            answerButton.interactable = true;
        }
    }

    private void Answer()
    {   
        StartCoroutine(Reply());
    }

    private IEnumerator Reply()
    {
        answerButton.interactable = false;

        if (currentChoosedAnswerIndex == currentQuiz.questions[currentQuestionIndex].correctOptionIndex)
        {
            int correct = PlayerPrefs.GetInt("TotalCorrectAnswers", 0);
            correct++;
            PlayerPrefs.SetInt("TotalCorrectAnswers", correct);

            replyButtons[currentChoosedAnswerIndex].GetComponent<Image>().sprite = correctReplyButtonSprite;
            pointers[currentQuestionIndex].sprite = correntQuestionPoint;
            results[currentQuestionIndex] = true;
        }
        else
        {
            int wrong = PlayerPrefs.GetInt("TotalWrongAnswers", 0);
            wrong++;
            PlayerPrefs.SetInt("TotalWrongAnswers", wrong);

            replyButtons[currentChoosedAnswerIndex].GetComponent<Image>().sprite = wrongReplyButtonSprite;
            pointers[currentQuestionIndex].sprite = wrongQuestionPoint;
            results[currentQuestionIndex] = false;
        }
        yield return new WaitForSeconds(2f);
        currentChoosedAnswerIndex = -1; // Reset the chosen answer index
        currentQuestionIndex++;
        SetQuestion();
    }


    private IEnumerator Timer()
    {
        float time = 60f; // Example timer duration
        while (time > 0)
        {
            time -= Time.deltaTime;
            timer.text = Mathf.Ceil(time).ToString();
            yield return null;
        }
        // Timer finished, handle end of quiz
        StopGame();
        
    }

    private void ShowResults()
    {
        ResultsData resultsData = new ResultsData
        {
            results = results,
            quizConfig = currentQuiz
        };
        UIManager.Instance.GetScreen(ScreenTypes.Results).SendData(resultsData);
        UIManager.Instance.ShowScreen(ScreenTypes.Results);
    }

    private void Close()
    {
        StopAllCoroutines();
        UIManager.Instance.ShowScreen(ScreenTypes.Categories);
    }
}
