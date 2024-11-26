using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    //[SerializeField] private bool closeTrigger = false;
    
    private bool IsOpen = false;

    // audio
    public AudioSource OpenSound;
    public AudioSource CloseSound;

    void Start(){
        //OpenSound = GetComponent<AudioSource>();
        
    }

    

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
