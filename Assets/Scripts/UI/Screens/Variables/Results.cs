using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : BasicScreen
{
    public Button close;
    public Button tryAgain;

    public Image[] results;
    public Sprite correctResult;
    public Sprite wrongResult;

    public EmojiResults[] emojiResults;
    [Serializable]
    public class EmojiResults
    {
        public Image[] emojiImage;
    }

    private ResultsData _resultsData = new();

    public void Start()
    {
        close.onClick.AddListener(Close);
        tryAgain.onClick.AddListener(TryAgain);
    
    }
    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
        tryAgain.onClick.RemoveListener(TryAgain);
    }
    public override void SendData(object data)
    {
        _resultsData = data as ResultsData;
    }
    public override void SetScreen()
    {
        for(int i = 0; i < _resultsData.results.Count; i++)
        {
            if (_resultsData.results[i])
            {
                results[i].sprite = correctResult;

            }
            else
            {
                results[i].sprite = wrongResult;

            }


            for (int j = 0; j < emojiResults[i].emojiImage.Length; j++)
            {
                emojiResults[i].emojiImage[j].gameObject.SetActive(false);
            }

            for (int j = 0; j < _resultsData.quizConfig.questions[i].questionEmoji.Count; j++)
            {
                emojiResults[i].emojiImage[j].gameObject.SetActive(true);
                emojiResults[i].emojiImage[j].sprite = _resultsData.quizConfig.questions[i].questionEmoji[j];
            }
        }
    }

    public override void ResetScreen()
    {
    }

    private void Close()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.MainMenu);
    }

    private void TryAgain()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Quiz);
    }
}
public class ResultsData
{
    public List<bool> results;
    public GameQuizConfig quizConfig;
}