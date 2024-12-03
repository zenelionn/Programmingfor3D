using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.VFX;

using UnityEngine.SceneManagement;
public class Cutscene3Manager : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button skipButton;
    [SerializeField] private float typingSpeed;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] GameObject Ophelia;
    [SerializeField] GameObject Rust;
    [SerializeField] GameObject Fish;
    [SerializeField] GameObject Chest;
    [SerializeField] GameObject Fezz;
    [SerializeField] Animator FishAnimator;
    [SerializeField] Animator rustAnimator;
    [SerializeField] Animator opheliaAnimator;
    [SerializeField] Animator fezzAnimator;
    [SerializeField] Animator chestAnimator;

    [SerializeField] List<string> rustAnimations = new List<string>();
    [SerializeField] List<string> fezzAnimations = new List<string>();
    [SerializeField] List<string> opheliaAnimations = new List<string>();

    [Header("VFX")]
    [SerializeField] private VisualEffect smoke;

    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [Header("Cameras")]
    [SerializeField] List<Camera> cameraList = new List<Camera>();
    private Camera currentCamera;
    

    private int shotNumber = 0;
    [SerializeField] private int shotTotal;

    public static bool isCutscene3Finished = false;

    void Start()
    {
        // initialise the queue
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);

        skipButton.onClick.AddListener(SkipCutscene);
        

        smoke.pause = true;

        

        loadingScreen.SetActive(false);
        SwitchCameras(shotNumber);
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
            opheliaAnimator.Play(opheliaAnimations[shotNumber]);
            fezzAnimator.Play(fezzAnimations[shotNumber]);
            if (shotNumber == 1){
                chestAnimator.Play("Chest Open");
                FishAnimator.Play("Fish Spin");
            }
            if (shotNumber == 5){
                Fezz.transform.eulerAngles = new Vector3(356.228241f,188.802979f,352.472443f);
                Fish.gameObject.SetActive(false);
            }

            if (shotNumber == 9){
                Rust.transform.eulerAngles = new Vector3(0f,171.679977f,0f);
            }

            if (shotNumber == 20){
                smoke.pause = false;
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

        isCutscene3Finished = true;
        StartCoroutine(LoadLevelASync(levelToLoad));
    }

     private void SkipCutscene(){
        EndDialogue();
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
