using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;



    private void Start(){
        loadingScreen.SetActive(false);
    }

    public void OnTriggerEnter(Collider other){
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));

    }

    IEnumerator LoadLevelASync(string levelToLoad){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        yield return null;
   }
}
