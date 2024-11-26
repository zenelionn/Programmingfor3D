using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateYAxis : MonoBehaviour
{
    [SerializeField] private float amplitude; // amount
    [SerializeField] private float frequency; // speed

    private float initialY;
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = initialY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
