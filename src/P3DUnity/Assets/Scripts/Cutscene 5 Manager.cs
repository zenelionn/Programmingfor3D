using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Cutscene5Manager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private  TMP_Text dialogueText;
    [SerializeField] private  Button nextButton;
    [SerializeField] private  Button skipButton;
    [SerializeField] private float typingSpeed = 1f;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] private GameObject Ophelia;
    [SerializeField] private GameObject Rust;
    [SerializeField] private GameObject Fezz;
    
    [Header("VFX")]
    [SerializeField] private VisualEffect smoke;
    

    [SerializeField] private Animator opheliaAnimator;
    [SerializeField] private Animator fezzAnimator;

    [SerializeField] private List<string> fezzAnimations = new List<string>();
    [SerializeField] private List<string> opheliaAnimations = new List<string>();

    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [Header("Cameras")]
    [SerializeField] List<Camera> cameraList = new List<Camera>();
    private Camera currentCamera;

    [Header("Audio")]
    [SerializeField] private AudioSource OpheliaBoop1;
    [SerializeField] private AudioSource OpheliaBoop2;
    [SerializeField] private int randomNum;
    [SerializeField] private List<string> whosTalking = new List<string>();
    private int talkingTotal;

    private int shotNumber = 0;
    [SerializeField] private int shotTotal;

    void Start()
    {
        // initialise the queue
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);

        skipButton.onClick.AddListener(SkipCutscene);

        loadingScreen.SetActive(false);
        SwitchCameras(shotNumber);
        shotNumber = 1;
        talkingTotal = shotTotal + 1;

        smoke.pause = true;

    
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
                randomNum = Random.Range(1,2);
                if (shotNumber != talkingTotal){
                    if ((randomNum == 1)&&(whosTalking[shotNumber] == "Ophelia")){
                        OpheliaBoop1.Play();
                    }
                    if ((randomNum == 2)&&(whosTalking[shotNumber] == "Ophelia")){
                        OpheliaBoop2.Play();
                    }
            }
                yield return new WaitForSeconds(typingSpeed);

            }

            isTyping = false;
            nextButton.gameObject.SetActive(true);
    }

    private void OnNextButtonClicked(){
        DisplayNextSentence();
        if (shotNumber != shotTotal){
            // change the animation

            opheliaAnimator.Play(opheliaAnimations[shotNumber]);
            fezzAnimator.Play(fezzAnimations[shotNumber]);

            if (shotNumber == 9){
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
