using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Cutscene6Manager : MonoBehaviour
{

    [Header("Text")]
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button skipButton;
    public float typingSpeed = 1f;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] GameObject Rust;
    [SerializeField] GameObject Fezz;

    [SerializeField] Animator fezzAnimator;
    [SerializeField] Animator rustAnimator;

    [SerializeField] List<string> fezzAnimations = new List<string>();
    [SerializeField] List<string> rustAnimations = new List<string>();


    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [Header("Cameras")]
    [SerializeField] List<Camera> cameraList = new List<Camera>();
    private Camera currentCamera;
    

    private int shotNumber = 0;
    [SerializeField] private int shotTotal;
    // Start is called before the first frame update
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
            fezzAnimator.Play(fezzAnimations[shotNumber]);

            if (shotNumber == 1){
                Rust.transform.position = new Vector3(7.2013588f,0.100000001f,-1.52657115f);
            }
            if (shotNumber == 4){
                Rust.transform.position = new Vector3(6.80000019f,0.0990689322f,-1.38f);
                Rust.transform.eulerAngles = new Vector3(0f,276.140015f,0f);
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
