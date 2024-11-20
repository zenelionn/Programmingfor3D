using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using StarterAssets;

public class Clue1 : MonoBehaviour
{

    [SerializeField] private bool hittingPlayer = false;

    public TMP_Text clueText;
    public GameObject clueCanvas;


    // Start is called before the first frame update
    void Start()
    {
        clueText.gameObject.SetActive(false);
        clueCanvas.SetActive(false);
    }

    void Update()
    {
        // if approaching the first clue, and E is pressed, open the clue!

        if (Input.GetKeyDown(KeyCode.E) && (hittingPlayer == true))
        {
            // set clue canvas 1 to active
            Interactable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            clueText.text = "Press E to read Clue";
            
            clueText.gameObject.SetActive(true);
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            clueText.gameObject.SetActive(false);
            hittingPlayer = false;
        }
    }

    public void Interactable()
    {
        clueCanvas.SetActive(true);
        clueText.gameObject.SetActive(false);

    }

    public void StopInteract()
    {
        clueCanvas.SetActive(false);
    }
}
