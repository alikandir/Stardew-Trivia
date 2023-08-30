using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
   [SerializeField] AudioSource audioSource;
   [SerializeField] AudioClip correctSound,wrongSound,endingSound;

   public void CorrectPlay()
   {
    audioSource.clip = correctSound;
    audioSource.Play();
   }
   public void WrongPlay()
   {
    audioSource.clip=wrongSound;
    audioSource.Play();
   }
   

}
