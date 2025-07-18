using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    public Button avatar;
    public TMP_InputField name;
    public TMP_Text coins;
    public TMP_Text totalCorectAnswers;
    public TMP_Text totalWrongAnswers;
    public TMP_Text totalOpenedAchievements;
    public Image[] achievements;
    public Sprite[] achievementSprites;

    public AvatarManager avatarManager;

    private void Start()
    {
        avatar.onClick.AddListener(() => avatarManager.PickFromGallery());
        
    }

    public override void Init()
    {
        base.Init();
        name.text = PlayerPrefs.GetString("Name", "Player");    
    }
    private void OnDestroy()
    {

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Name", name.text);
    }

    public override void ResetScreen()
    {
        if(name.text != PlayerPrefs.GetString("Name", "Player"))
        {
            PlayerPrefs.SetString("Name", name.text);
        }
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        name.text = PlayerPrefs.GetString("Name", "Player");
        coins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        totalCorectAnswers.text = PlayerPrefs.GetInt("TotalCorrectAnswers", 0).ToString();
        totalWrongAnswers.text = PlayerPrefs.GetInt("TotalWrongAnswers", 0).ToString();

        int totalAchievements = 0;
        for (int i = 0;i < achievements.Length; i++)
        {
            if (PlayerPrefs.GetInt("Achievement" + i, 0) == 1)
            {
                totalAchievements++;
                achievements[i].sprite = achievementSprites[i];
            }
        }
        totalOpenedAchievements.text = totalAchievements.ToString();
    }

}
