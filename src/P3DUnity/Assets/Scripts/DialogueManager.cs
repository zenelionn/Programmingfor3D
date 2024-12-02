using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button skipButton;
    public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] Animator rustAnimator;
    [SerializeField] Animator fezzAnimator;
    [SerializeField] List<string> rustAnimations = new List<string>();
    [SerializeField] List<string> fezzAnimations = new List<string>();

    private int animation = 0;

    [Header("Cameras")]
    [SerializeField] Camera rustCamera;
    [SerializeField] Camera fezzCamera;

    [Header("Level Loader")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string levelToLoad;

    [SerializeField] List<Camera> CamerasType = new List<Camera>();

    // Start is called before the first frame update
    void Start()
    {
        // initialise the queue
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(OnNextButtonClicked);
        skipButton.onClick.AddListener(SkipCutscene);
        nextButton.gameObject.SetActive(false);

        // initialise cameras
        rustCamera.enabled = true;
        fezzCamera.enabled = false;

        loadingScreen.SetActive(false);
        
    
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

    public void PlayNextAnimation(){
        if (animation > 8){
            animation = 8;
        }
        else{
            animation = animation + 1;
        }
        
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
        // play next animation
        if (animation > 8){
            rustAnimator.Play("Idle");
            fezzAnimator.Play("Sitting Idle");
        }
        else{
            rustAnimator.Play(rustAnimations[animation]);
            fezzAnimator.Play(fezzAnimations[animation]);

            // switch cameras
            if(CamerasType[animation] == rustCamera){
                rustCamera.enabled = true;
                fezzCamera.enabled = false;
            }
            if(CamerasType[animation] == fezzCamera){
                rustCamera.enabled = false;
                fezzCamera.enabled = true;
            }

            PlayNextAnimation();


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

    IEnumerator LoadLevelASync(string levelToLoad){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        yield return null;
   }
}
