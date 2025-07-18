using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameQuizConfig", menuName = "Game/GameQuizConfig")]
public class GameQuizConfig : ScriptableObject
{
    public List<Question> questions;

    [Serializable]
    public class Question
    {
        public List<Sprite> questionEmoji;
        public List<string> options;
        public int correctOptionIndex;
    }
}
