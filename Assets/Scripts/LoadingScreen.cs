using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string sceneName; // ����� �����, ��� ������� �����������
    [SerializeField] private TMP_Text progressText; // �������� ���� ��� ������� ������������
    [SerializeField] private Image progressBar; // ���������� �������-����
    [SerializeField] private Slider sliderBar; 
    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // ������ ���������� ������������

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // ��������� �� ����� 0-100%
            if (progressText != null)
            {
                progressText.text = $"{progress * 100:F0}%";
            }
            if (progressBar != null)
            {
                progressBar.fillAmount = progress;
            }
            if(sliderBar != null)
            {
                sliderBar.value = progress;
            }

            if (progress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
