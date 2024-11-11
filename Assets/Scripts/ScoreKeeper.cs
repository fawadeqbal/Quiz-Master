using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int questionSeen=0;
    int correctAnswer=0;
    
    public int GetCorrectAnswers(){
        return this.correctAnswer;
    }
    public int GetQuestionSeen(){
        return this.questionSeen;
    }
    public void IncrementCorrectAnswer(){
        correctAnswer++;
    }
    public void IncrementQuestionSeen(){
        questionSeen++;
    }
    public int CalculateScore(){
        return Mathf.RoundToInt((correctAnswer/(float)questionSeen)*100);
    }
}
