using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTimer : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 10f;
    [SerializeField] private string sceneNameToLoad;
    private float TimeElapsed;



    // Update is called once per frame
    private void Update()
    {
        TimeElapsed += Time.deltaTime;

        if (TimeElapsed > delayBeforeLoading){
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
