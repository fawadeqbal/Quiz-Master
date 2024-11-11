using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionSO", menuName = "QuestionSO", order = 0)]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 4)]
    [SerializeField] string question = "this is question";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnsIndex;

    public string GetQuestion()
    {
        return question;
    }
    public int GetCorrectAnswerIndex()
    {
        return correctAnsIndex;
    }
    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public static implicit operator List<object>(QuestionSO v)
    {
        throw new NotImplementedException();
    }
}
