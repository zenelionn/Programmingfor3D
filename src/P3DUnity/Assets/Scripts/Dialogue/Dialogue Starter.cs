using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueStarter : MonoBehaviour
{

    public DialogueManager dialogueManager;  // Reference to the DialogueManager
    public Dialogue dialogue;               // Reference to the Dialogue ScriptableObjec
    [SerializeField] private Button startButton;

    private void Start(){
        startButton.onClick.AddListener(StartCutscene);
    }

    public void StartCutscene(){
        startButton.gameObject.SetActive(false);
        dialogueManager.StartDialogue(dialogue);

    }
}
