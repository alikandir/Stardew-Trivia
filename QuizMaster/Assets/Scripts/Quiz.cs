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
    bool hasAnsweredEarly=true;
    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [Header("Slider")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    SoundEffects sound;

    void Awake()
    {
        timer=FindObjectOfType<Timer>();
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
        sound=FindObjectOfType<SoundEffects>();
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
            SetButtonState(false);
            DisplayCorrectAnswer(-1); 
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
        if (index==currentQuestion.GetCorrectAnswerIndex()&&hasAnsweredEarly)
        {
            questionText.text="Correct Answer!";
            sound.CorrectPlay();
            Image buttonImage=answerButtons[index].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else 
        {
            
            correctAnswerIndex=currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer=currentQuestion.GetAnswer(correctAnswerIndex);            
            questionText.text="Sorry the correct answer was:\n"+ correctAnswer;
            sound.WrongPlay();

            Image buttonImageCorrect=answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImageCorrect.sprite=correctAnswerSprite;
            if(hasAnsweredEarly)
            {  
                Image buttonImageWrong=answerButtons[index].GetComponent<Image>();
                buttonImageWrong.sprite=wrongAnswerSprite;
            }
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

