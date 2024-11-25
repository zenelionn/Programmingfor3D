using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitsPlayer : MonoBehaviour
{

    
    [SerializeField] private bool hittingPlayer;
    [SerializeField] private CharacterController player;

    private Vector3 originalPosition;


    void Start(){
        originalPosition = player.transform.position;
    } 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.enabled = false;
            player.transform.position = originalPosition;
            player.enabled = true;
            //Debug.Log("move");
        }
    }
}
