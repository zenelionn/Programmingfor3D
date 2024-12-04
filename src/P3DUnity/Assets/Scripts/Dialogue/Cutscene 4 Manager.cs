using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene4Manager : MonoBehaviour
{

    [Header("Text")]
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button skipButton;
    public float typingSpeed = 1f;

    private bool isTyping = false;
    private Queue<string> sentences;

    [Header("Animations")]
    [SerializeField] GameObject Ophelia;
    [SerializeField] GameObject Rust;
    [SerializeField] GameObject Fezz;
    
    
    [SerializeField] Animator rustAnimator;
    [SerializeField] Animator opheliaAnimator;
    [SerializeField] Animator fezzAnimator;
    [SerializeField] List<string> rustAnimations = new List<string>();
    [SerializeField] List<string> fezzAnimations = new List<string>();
    [SerializeField] List<string> opheliaAnimations = new List<string>();

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
