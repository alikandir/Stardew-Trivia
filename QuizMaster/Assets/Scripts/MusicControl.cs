using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
   [SerializeField] GameObject nextSong;
   [SerializeField] GameObject stopStart;
   [SerializeField] AudioSource audioSource;
   [SerializeField] AudioClip[] clips;
   int clipIndex=0;
   bool onOffSwitch=true;

    void Start()
    {
        MusicPlayer();
    }
    
     public void MusicPlayer()
     {
        audioSource.clip=clips[clipIndex];
        audioSource.Play();
     }

     public void NextSong()
     {
        clipIndex++;
        audioSource.clip=clips[clipIndex];
        audioSource.Play();
        
     }
     public void StopStart()
     {
        if (onOffSwitch)
        {
            audioSource.Pause();
            onOffSwitch=false;
        }
        else
        {
            audioSource.UnPause();
            onOffSwitch=true;
        }
        
     }



}
