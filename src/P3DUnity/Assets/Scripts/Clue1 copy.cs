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

    public TMP_Text clue1Text;
    public GameObject Clue1Canvas;


    // Start is called before the first frame update
    void Start()
    {
        clue1Text.gameObject.SetActive(false);
        Clue1Canvas.SetActive(false);
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
            clue1Text.text = "Press E to read Clue";
            clue1Text.gameObject.SetActive(true);
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            clue1Text.gameObject.SetActive(false);
            hittingPlayer = false;
        }
    }

    public void Interactable()
    {
        Clue1Canvas.SetActive(true);

    }

    public void StopInteract()
    {
        Clue1Canvas.SetActive(false);
    }
}
