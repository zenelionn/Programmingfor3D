using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene2starter : MonoBehaviour
{
    public Cutscene2Manager cutsceneManager;
    public Dialogue dialogue;

    [SerializeField] private Button startbutton;

    private void Start(){
        startbutton.onClick.AddListener(OnNextButtonClicked);
        
    }

    private void OnNextButtonClicked(){
        Debug.Log("sigma");
        cutsceneManager.StartDialogue(dialogue);
        startbutton.gameObject.SetActive(false);
    }
}
