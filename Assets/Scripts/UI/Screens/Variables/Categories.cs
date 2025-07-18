using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Categories : BasicScreen
{
    public Button close;
    public Button StartQuiz;

    public Button[] categoryButtons;
    public Image[] categorySelectImages;
    public GameObject[] categoriesSelectIndicators;


    private int _currentCategoryIndex = 0;

    private void Start()
    {
       
        close.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.MainMenu));
        StartQuiz.onClick.AddListener(StartQuizGame);
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            int index = i; 
            categoryButtons[i].onClick.AddListener(() => CategorySelected(index));
        }
    }
    private void OnDestroy()
    {
        close.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.MainMenu));
        StartQuiz.onClick.RemoveListener(StartQuizGame);
        for (int i = 0; i < categoryButtons.Length; i++)
        {
            categoryButtons[i].onClick.RemoveAllListeners();
        }
    }
    public override void ResetScreen()
    {
        StartQuiz.interactable = false; // Initially disable the StartQuiz button
        // Deselect the previous category
        if (_currentCategoryIndex >= 0 && _currentCategoryIndex < categoryButtons.Length)
        {
            categorySelectImages[_currentCategoryIndex].enabled = false;
            categoriesSelectIndicators[_currentCategoryIndex].SetActive(false);
            StartQuiz.interactable = false; // Disable the StartQuiz button when changing categories
        }
    }

    public override void SetScreen()
    {
        
    }

    private void StartQuizGame()
    {
        UIManager.Instance.GetScreen(ScreenTypes.Quiz).SendData(_currentCategoryIndex);
        UIManager.Instance.ShowScreen(ScreenTypes.Quiz);
    }
    public void CategorySelected(int index)
    {
        if (index < 0 || index >= categoryButtons.Length)
        {
            Debug.LogError("Invalid category index selected: " + index);
            return;
        }
        // Deselect the previous category
        if (_currentCategoryIndex >= 0 && _currentCategoryIndex < categoryButtons.Length)
        {
            categorySelectImages[_currentCategoryIndex].enabled = false;
            categoriesSelectIndicators[_currentCategoryIndex].SetActive(false);
            StartQuiz.interactable = false; // Disable the StartQuiz button when changing categories
        }
        // Select the new category
        _currentCategoryIndex = index;
        categorySelectImages[_currentCategoryIndex].enabled = true;
        categoriesSelectIndicators[_currentCategoryIndex].SetActive(true);
        StartQuiz.interactable = true; // Enable the StartQuiz button when a category is selected
    }
}
