using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private GameObject frontDoorBarrier;



    // Start is called before the first frame update
    void Start()
    {
        playerText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cutscene2Manager.isCutscene2Finished == true){
            Destroy(gameObject);
        }
        


        
    }

    private void OnTriggerEnter(Collider other){
        // change text
        if (other.gameObject.CompareTag("Player")){
            Debug.Log("hitting");
            playerText.text = "I'm pretty certain the pillow is in my room";
             playerText.gameObject.SetActive(true);
        }
        

    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Player")){
            
             playerText.gameObject.SetActive(false);
        }
    }
}


// if approaching the front door barrier or the back door barrier:
        // show up player canvas saying:
        // "I'm pretty certain the pillow is in my room"