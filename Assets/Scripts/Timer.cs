using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompeleteQuestion=30f;
    [SerializeField] float timeToShowCorrectAnswer=10f;
    float timerValue;
    public float fillFraction;
    public bool loadNextQuestion;
    public bool isAnsweringQuesion = false;
    public void Update()
    {
        UpdateTimer();
    }
    public void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuesion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue/timeToCompeleteQuestion;

            }
            else
            {
                isAnsweringQuesion = false;
                timerValue = timeToShowCorrectAnswer;
            }

        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue/timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuesion = true;
                timerValue = timeToCompeleteQuestion;
                loadNextQuestion=true;
            }
        }
        Debug.Log("Time Value" + timerValue);

    }
    public void CancelTimer(){
        timerValue = 0;
    }
}
