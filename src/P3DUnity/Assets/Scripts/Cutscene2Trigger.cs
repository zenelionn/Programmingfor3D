using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene2Trigger : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    private Start(){
        loadingScreen.SetActive(false);
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            // load async cutscene 2
            loadingScreen.SetActive(true);
            

        }
    }
}

// if hitting async load cutscene 2 scene
