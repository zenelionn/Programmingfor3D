using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;   
    private bool IsOpen = false;

    // audio
    [SerializeField] private  AudioSource OpenSound;
    [SerializeField] private AudioSource CloseSound;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if (openTrigger){
                myDoor.Play("DoorOpen", 0, 0.0f);
                OpenSound.Play(0);
   
            }           
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            if (openTrigger && IsOpen == false){  
                myDoor.Play("DoorClose", 0, 0.0f);
                CloseSound.Play(0);                
            }
        }
    }
}
