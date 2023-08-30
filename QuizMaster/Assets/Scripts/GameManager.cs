using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    Ending endScreen;
    StartScreen start;
    Credits credits;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip endingSound;
    bool soundStopper=true;

    void Awake()
    {
        quiz=FindObjectOfType<Quiz>();
        endScreen=FindObjectOfType<Ending>();
        start=FindObjectOfType<StartScreen>();
        credits=FindObjectOfType<Credits>();
        
        
   
    }
    void Start()
    {

        start.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false); 
        credits.gameObject.SetActive(false);   

    }

    void Update()
    {
        if(quiz.isComplete)
        {
            
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            if(soundStopper)
            {
                audioSource.clip=endingSound;
                audioSource.Play();
                soundStopper=false;
            }
           
        }
    }
    public void OnReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
        public void OnStartButton()
    {
        start.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
    }

    public void OnCreditsButton()
    {
        start.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }
    public void OnMainMenuButton()
    {
        start.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
    }
    }
