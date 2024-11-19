using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private  bool interactable = true;
    [SerializeField] private bool hittingPlayer;
    [SerializeField] private GameObject lockCanvas;
    [SerializeField] private TMP_Text[] Text;
    [SerializeField] private string Password;
    [SerializeField] private string[] LockCharacterChoices;
    [SerializeField] private int[] LockCharacterNumber;

    private string insertedPassword;
    
    // Start is called before the first frame update
    void Start()
    {
        LockCharacterNumber = new int[Password.Length];
        UpdateUI();
        hittingPlayer = false;
    }

    public void ChangeInsertedPassword(int number)
    {
        LockCharacterNumber[number]++;
        if (LockCharacterNumber[number] >= LockCharacterChoices[number].Length)
        {
            LockCharacterNumber[number] = 0;   
        }

        Checkpassword();
        UpdateUI();
    }

    public void Checkpassword()
    {
        int pass_len = Password.Length;
        insertedPassword = "";

        for(int i=0; i < pass_len; i++)
        {
            insertedPassword += LockCharacterChoices[i][LockCharacterNumber[i]].ToString();
        }

        if(Password == insertedPassword)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        Debug.Log("Unlocked");
    }

    public void UpdateUI()
    {
        int len = Text.Length;
        for (int i=0; i < len; i++)
        {
            Text[i].text = LockCharacterChoices[i][LockCharacterNumber[i]].ToString();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (hittingPlayer == true))
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hittingPlayer = false;
        }
    }


    public void Interact()
    {
        if (interactable)
        {
            lockCanvas.SetActive(true);
        }
        
    }

    public void StopInteract()
    {
        lockCanvas.SetActive(false);
    }

}
