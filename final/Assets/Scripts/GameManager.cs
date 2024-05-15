using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public Canvas winCanvas, loseCanvas;
    public Objetivos objetivos;


    public int defeatedEnemies = 0;
    public int activeGenerators = 0;
    public int keyCard = 0;

    [SerializeField] private bool isPaused = false;
    public Canvas pauseCanvas;
    public GameObject uiPlayer;
    public Slider sliderMusic, sliderSFX, sliderMaster;
    
    public Slider sensitivitySlider;
    public CameraController cameraController;
    public bool PlayerWin = false;

    public bool IsPaused
    {
        get { return isPaused; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        sensitivitySlider.value = cameraController.mouseSensitivity;
        sensitivitySlider.onValueChanged.AddListener(cameraController.SetMouseSensitivity);


        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        PausePanel();

        // if(uiPlayer == null){
        //     uiPlayer = GameObject.Find("UIPlayer");
        // }
    }

    public void PauseGame()
    {
        uiPlayer.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        isPaused = true;
        
    }
   
    public void ResumeGame()
    {
        uiPlayer.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isPaused = false;
        
    }

    public void ResetGame()
    {
        ResumeGame();
        defeatedEnemies = 0;
        activeGenerators = 0;
        keyCard = 0;
        objetivos.currentObjective = 0;
        objetivos.activeObjectives = new();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        defeatedEnemies = 0;
        SceneManager.LoadScene("Main Menu");
    }

    public void LosePlayer()
    {
        loseCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void WinPlayer()
    {
        PlayerWin = true;
        winCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    void PausePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {      
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {       
            ResumeGame();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }


}
