using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{

    public DialogueManager dialogueManager;  // Reference to the DialogueManager
    public Dialogue dialogue;               // Reference to the Dialogue ScriptableObject

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))  // Start dialogue on spacebar press
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
}
