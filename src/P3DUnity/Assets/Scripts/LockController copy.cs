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
    [SerializeField] private TMP_Text[] text;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private string password;
    [SerializeField] private string[] lockCharacterChoices;
    [SerializeField] private int[] lockCharacterNumber;

    private string insertedPassword;
    
    // Start is called before the first frame update
    void Start()
    {
        playerText.gameObject.SetActive(false);
        lockCharacterNumber = new int[password.Length];
        UpdateUI();
        playerText.gameObject.SetActive(false);
        hittingPlayer = false;
    }

    public void ChangeInsertedPassword(int number)
    {
        lockCharacterNumber[number]++;
        if (lockCharacterNumber[number] >= lockCharacterChoices[number].Length)
        {
            lockCharacterNumber[number] = 0;   
        }

        Checkpassword();
        UpdateUI();
    }

    public void Checkpassword()
    {
        int passLen = password.Length;
        insertedPassword = "";

        for(int i=0; i < passLen; i++)
        {
            insertedPassword += lockCharacterChoices[i][lockCharacterNumber[i]].ToString();
        }

        if(password == insertedPassword)
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
        int len = text.Length;
        for (int i=0; i < len; i++)
        {
            text[i].text = lockCharacterChoices[i][lockCharacterNumber[i]].ToString();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (hittingPlayer == true))
            Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerText.gameObject.SetActive(true);
        if (other.gameObject.CompareTag("Player"))
        {
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerText.gameObject.SetActive(false);
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
