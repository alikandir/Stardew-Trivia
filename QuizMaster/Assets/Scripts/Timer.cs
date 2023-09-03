using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion=30f;
    [SerializeField] float timeToShowCorrectAnswer=10f;
    Quiz quiz;
    public bool isAnsweringQuestion=true;
    public float fillFraction;
    public bool loadNextQuestion;

    float timerValue;
    void Awake()
    {
        quiz=FindObjectOfType<Quiz>();
    }
    void Update()
    {
        if(quiz.gameObject.activeInHierarchy)
        {
            UpdateTimer();
        }
        
    }
    public void CancelTimer()
    {
        timerValue=0;
    }

    void UpdateTimer()
    {
        timerValue-=Time.deltaTime;
        if(isAnsweringQuestion)
        {
             if(timerValue > 0)
            {
                fillFraction=timerValue/timeToCompleteQuestion;
            }
             else
            {
                isAnsweringQuestion=false;
                timerValue=timeToShowCorrectAnswer;
            }

        }

        else
        {

        
            {
                if(timerValue > 0)
                {
                    fillFraction=timerValue/timeToShowCorrectAnswer;
                }
                else
                {
                    isAnsweringQuestion=true;
                    timerValue=timeToCompleteQuestion;
                    loadNextQuestion=true;
                }
            }
         }
        
    }
    
}
