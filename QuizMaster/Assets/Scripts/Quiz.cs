using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;
    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [Header("Slider")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    void Awake()
    {
        timer=FindObjectOfType<Timer>();
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue=questions.Count;
        progressBar.value=0;  
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value==progressBar.maxValue)
            {
                isComplete=true;
                return;
            }
            hasAnsweredEarly=false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayCorrectAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayCorrectAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text="Score:"+scoreKeeper.CalculateScore()+"%";
    }
        

    void DisplayCorrectAnswer(int index)
    {
        if (index==currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text="Correct Answer!";
            Image buttonImage=answerButtons[index].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {
            correctAnswerIndex=currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer=currentQuestion.GetAnswer(correctAnswerIndex);            
            questionText.text="Sorry the correct answer was:\n"+ correctAnswer;
            
            Image buttonImage=answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
        }
    }
       


    void GetNextQuestion()
    {
        if(questions.Count>0)
        {
            hasAnsweredEarly=false;
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestions();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
        else
        {
            questionText.text="Game Over";
        }
    }
    void GetRandomQuestion()
    {
        int index= Random.Range(0,questions.Count);
        currentQuestion=questions[index];
        if (questions.Contains(currentQuestion)) //Check first to avoid any mistakes, generally do this!
        {
             questions.Remove(currentQuestion);
        }
    }
    void DisplayQuestions()
    {
        questionText.text=currentQuestion.GetQuestion();  
        for (int i=0;i<answerButtons.Length;i++)
        {
            TextMeshProUGUI buttonText=answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text=currentQuestion.GetAnswer(i);
        } 
    }

    void SetButtonState(bool state)
    {
        for (int i=0;i<answerButtons.Length;i++)
        {
            Button button= answerButtons[i].GetComponent<Button>();
            button.interactable=state;
        }
    }
    void SetDefaultButtonSprite()
    {
        for (int i=0;i<answerButtons.Length;i++)
        {
            Image buttonImage=answerButtons[i].GetComponent<Image>();
            buttonImage.sprite=defaultAnswerSprite;
        }
    }
}

