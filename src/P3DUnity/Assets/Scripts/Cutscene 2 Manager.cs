using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class Cutscene2Manager : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text dialogueText;
    public Button nextButton;
    public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] Animator rustAnimator;
    [SerializeField] List<string> rustAnimations = new List<string>();
    [SerializeField] List<string> fezzAnimations = new List<string>();

    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [Header("Cameras")]
    [SerializeField] List<Camera> cameraList = new List<Camera>();
    private Camera currentCamera;

    private int shotNumber;
    [SerializeField] private int shotTotal;

    void Start()
    {
        // initialise the queue
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);

        loadingScreen.SetActive(false);

        shotNumber = 1;

        // initialise animation
        rustAnimator.Play("Idle");
        

        
        
    
    }

    public void StartDialogue(Dialogue dialogue){
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence(){
        if(isTyping){
            return;
        }

        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));

    }

        

private IEnumerator TypeSentence(string sentence){
        dialogueText.text = ""; // clears current text
        isTyping = true;
        nextButton.gameObject.SetActive(false);

        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }

        isTyping = false;
        nextButton.gameObject.SetActive(true);
    }

    private void OnNextButtonClicked(){
        DisplayNextSentence();
        if (shotNumber != shotTotal){
            // change the animation
            rustAnimator.Play(rustAnimations[shotNumber]);
            // change camera
            SwitchCameras(shotNumber);

            shotNumber = shotNumber + 1;
            Debug.Log(shotNumber);
        }
        else{
            shotNumber = shotTotal;
        }

        
        
    }

    private void EndDialogue(){
        Debug.Log("End of Dialogue");
        nextButton.gameObject.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    private void SwitchCameras(int shotNumber){
        foreach (Camera cam in cameraList){
            cam.gameObject.SetActive(false);
        }

        cameraList[shotNumber].gameObject.SetActive(true);
    }

    IEnumerator LoadLevelASync(string levelToLoad){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        yield return null;
   }
}
