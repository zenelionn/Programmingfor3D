using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using StarterAssets;

public class lockcheck : MonoBehaviour
{

    public TMP_Text lockText;

    // Start is called before the first frame update
    void Start()
    {
        // hide the current text
        lockText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lockText.text = "Press E to enter the code";
            lockText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lockText.gameObject.SetActive(false);
        }
    }

}
