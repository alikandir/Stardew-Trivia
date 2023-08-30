using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI endingText;
    Button restartButton;
    ScoreKeeper scoreKeeper;

    void Awake ()
    {
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        endingText.text="Congratulations!\nYour score is: "+ scoreKeeper.CalculateScore()+"%";
    }
}
