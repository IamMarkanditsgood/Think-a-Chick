using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BasicScreen
{
    public Button startQuiz;
    public TMP_Text name;
    public TMP_Text coins;
    public AvatarManager avatarManager;

    private void Start()
    {
        startQuiz.onClick.AddListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Categories));

    }

    private void OnDestroy()
    {
        startQuiz.onClick.RemoveListener(() => UIManager.Instance.ShowScreen(ScreenTypes.Categories));
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        name.text = PlayerPrefs.GetString("Name", "Player");
        coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

}
