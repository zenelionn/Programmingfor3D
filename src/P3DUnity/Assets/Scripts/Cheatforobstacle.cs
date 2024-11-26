using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatforobstacle : MonoBehaviour
{

    [SerializeField] private CharacterController player;
    [SerializeField] private GameObject finalPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.C))
        {
            player.enabled = false;
            player.transform.position = finalPosition.transform.position;
            player.enabled = true;
        }  
    }
}
