using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers=0;
    int questionsSeen=0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        //float in paranthesis "casts" the questionsSeen to become float rather than int.
        //This is necessary for getting an percentage value back. 3/4 -> would have resulted 0 if both of them are integers.
        int score= Mathf.RoundToInt(correctAnswers/ (float) questionsSeen * 100); 
        return score;
    }
}
