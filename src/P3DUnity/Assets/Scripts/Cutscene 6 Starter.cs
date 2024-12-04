using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene6starter : MonoBehaviour
{
    public Cutscene6Manager cutsceneManager;
    public Dialogue dialogue;

    [SerializeField] private Button startbutton;

    private void Start(){
        startbutton.onClick.AddListener(OnNextButtonClicked);
        
    }

    private void OnNextButtonClicked(){

        cutsceneManager.StartDialogue(dialogue);
        startbutton.gameObject.SetActive(false);
    }
}
