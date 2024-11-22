using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FezziwigFollow : MonoBehaviour
{

    [SerializeField] private NavMeshAgent fezziwig;
    [SerializeField] private Transform player;
    [SerializeField] private Transform fezziwigTransform;
    [SerializeField] private Animator fezziwigAnimator;
    
    public float stopDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        fezziwig.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= stopDistance){
            fezziwigAnimator.SetTrigger("StopChasing");
        }
        else{
            fezziwigAnimator.SetTrigger("StartChasing");
        }
        

    
        

    }
}
