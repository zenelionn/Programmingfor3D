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
    [SerializeField] GameObject Ophelia;
    [SerializeField] GameObject Rust;
    [SerializeField] GameObject Fish;
    [SerializeField] Animator FishAnimator;
    [SerializeField] Animator rustAnimator;
    [SerializeField] Animator opheliaAnimator;
    [SerializeField] List<string> rustAnimations = new List<string>();
    [SerializeField] List<string> fezzAnimations = new List<string>();
    [SerializeField] List<string> opheliaAnimations = new List<string>();

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

        Ophelia.transform.position = new Vector3(13.7f,0.631f,0.064f);
        Fish.transform.position = new Vector3(7.45265007f,0.0769999996f,-1.29499996f);
        Fish.transform.eulerAngles = new Vector3(0f,341.634491f,0f);

        
        
    
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
            opheliaAnimator.Play(opheliaAnimations[shotNumber]);

            // check shots for positions for ophelia
            if (shotNumber == 2){
                Ophelia.transform.position = new Vector3(14.065030f,1.13371289f,1.19528151f);
                Ophelia.transform.eulerAngles = new Vector3(357.077789f,299.856567f,0.00447649974f);
            }
            if (shotNumber == 4){
                Ophelia.transform.position =  new Vector3(6.80288839f,-0.868864596f,-1.02460706f);
                Ophelia.transform.eulerAngles = new Vector3(357.077759f,53.2262421f,0.00448643835f);
                Rust.transform.eulerAngles = new Vector3(0f,70.6999969f,0f);
            }
            if (shotNumber == 5){
                Rust.transform.eulerAngles = new Vector3(0f,238.149506f,0f);
            }
            if (shotNumber == 6){
                Ophelia.transform.position = new Vector3(6.50184679f,-0.487999976f,-0.602499127f);
                Ophelia.transform.eulerAngles = new Vector3(357.077881f,71.9102249f,0.0302427374f);
            }
            if (shotNumber == 9){
                Ophelia.transform.eulerAngles = new Vector3(357.077881f,92.4920044f,0.0302289501f);
                Fish.transform.eulerAngles = new Vector3(57.6701889f,358.568634f,328.362183f);
                FishAnimator.Play("Fish Float");
            }

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
