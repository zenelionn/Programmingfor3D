using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class Cutscene2Manager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private float typingSpeed = 1f;
    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] private GameObject Ophelia;
    [SerializeField] private GameObject Rust;
    [SerializeField] private GameObject Fish;
    [SerializeField] private Animator FishAnimator;
    [SerializeField] private Animator rustAnimator;
    [SerializeField] private Animator opheliaAnimator;
    [SerializeField] private List<string> rustAnimations = new List<string>();
    [SerializeField] private List<string> fezzAnimations = new List<string>();
    [SerializeField] private List<string> opheliaAnimations = new List<string>();

    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [Header("Cameras")]
    [SerializeField] private List<Camera> cameraList = new List<Camera>();
    private Camera currentCamera;
    
    [Header("Audio")]
    [SerializeField] private AudioSource RustBoop1;
    [SerializeField] private AudioSource RustBoop2;
    [SerializeField] private AudioSource OpheliaBoop1;
    [SerializeField] private AudioSource OpheliaBoop2;
    [SerializeField] private int randomNum;
    [SerializeField] private List<string> whosTalking = new List<string>();
    private int talkingTotal;

    private int shotNumber = 0;
    [SerializeField] private int shotTotal;

    private static bool isCutscene2Finished = false;
    public static bool IsCutscene2Finished   // property
    {
        get { return isCutscene2Finished; }   // get method
        set { IsCutscene2Finished = value; }  // set method
    }

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
                randomNum = Random.Range(1,2);
                if (shotNumber != talkingTotal){
                    if ((randomNum == 1)&&(whosTalking[shotNumber] == "Rust")){
                    RustBoop1.Play();
                    }
                    if ((randomNum == 2)&&(whosTalking[shotNumber] == "Rust")){
                        RustBoop2.Play();
                    }
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
        if (shotNumber != talkingTotal){
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
            if (shotNumber == 18){
                Ophelia.transform.eulerAngles = new Vector3(3.46166492f,70.0800476f,357.566101f);
            }
            if (shotNumber == 19){
                Ophelia.transform.position = new Vector3(0,0,0);
                FishAnimator.Play("Fish still");
                Fish.SetActive(false);
            }
            if (shotNumber == 21){
                Rust.transform.position = new Vector3(7.40705442f,-0.273988038f,-0.297263324f);
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

        isCutscene2Finished = true;
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
