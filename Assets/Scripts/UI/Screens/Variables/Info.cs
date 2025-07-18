using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : BasicScreen
{
    public Button close;

    private void Start()
    {
        close.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Settings));
    }
    private void OnDestroy()
    {
        close.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Settings));
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }
}
