using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottom : MonoBehaviour
{
    public Button home;
    public Button profile;
    public Button settings;

    private void Start()
    {
        home.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.MainMenu));
        profile.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Profile));
        settings.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Settings));
    }
    public void OnDestroy()
    {
        home.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.MainMenu));
        profile.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Profile));
        settings.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Settings));
    }

}
