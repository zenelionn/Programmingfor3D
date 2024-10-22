using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool reOpenTrigger = false;
    private bool IsOpen = false;

    //public GameObject reOpenTriggerOBJ;

    

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if (openTrigger){
                myDoor.Play("DoorOpen", 0, 0.0f);
                //IsOpen = true;
                //gameObject.SetActive(false);
                
                
            }
        
            //else if (closeTrigger &&sOpen == true){
                //myDoor.Play("DoorClose", 0, 0.0f);
                //IsOpen = false;   
                //gameObject.SetActive(false);
            //} 
            //else if (closeTrigger && IsOpen == false){
                //myDoor.Play("DoorOpen", 0, 0.0f);
            //}
            
        }


    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            if (openTrigger && IsOpen == false){
                
                myDoor.Play("DoorClose", 0, 0.0f);
                //IsOpen = false;
                //gameObject.SetActive(false);
                
                
            }
            
        }

    }
}
