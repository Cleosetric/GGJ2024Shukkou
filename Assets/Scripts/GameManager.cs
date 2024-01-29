using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public int startingLives = 3; // Number of starting lives
    public float minDeathSpeed = 5;
    public float distanceThreshold = 10f; // Distance threshold for each slider increment
    public float maxDistancePower = 100;
    [Header("Object Reference")]
    [Space]
    public GameObject player;
    public GameObject playerUI;
    public GameObject gameOverUI;
    public Transform firstPoint;
    [Header("UI Reference")]
    [Space]
    public TextMeshProUGUI textScoreGameOver;
    public TextMeshProUGUI textScore;
    public List<Image> liveIcons;
    public Slider sliderPower;

    private static GameManager instance;
    private bool isPowerActive;
    private bool isGameStart;
    private int lives; 
    private float distance;
    private float distancePowerUp;
    private Vector3 lastDistancePoint;


    // Singleton pattern to ensure only one instance of GameManager exists
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    instance = singleton.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        isGameStart = false;
        isPowerActive = false;

        if(player == null){
            player = FindObjectOfType<PlayerController>().gameObject;
        }
        player.GetComponent<PlayerController>().ResetRotation();
        player.transform.position = new Vector3(35,6,0);
        player.GetComponent<PlayerController>().FreezeControl();
        player.GetComponent<PlayerController>().enabled = false;
        playerUI.gameObject.SetActive(false);

        // foreach (Image iconImage in liveIcons)
        // {
        //     iconImage.color = new Color32(255,255,255,255);
        // }
        gameOverUI.SetActive(false);
    }
    
    private void Update(){
        if(!isGameStart){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartGame();
            }
        }

        if(isGameStart){
            CalculatePointDistance();
        }
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if(isGameStart == true){

        if(player.GetComponent<PlayerController>().IsPlayerDead()){
            EndGame();
        }
        }
    }

    private void CalculatePointDistance()
    {
        if(player != null)
        {
            // Calculate the distance between the player and the first point
            distance = Vector3.Distance(player.transform.position, firstPoint.position);
            // Convert the distance to meters (assuming 1 Unity unit = 1 meter)
            float distanceInMeters = distance / distanceThreshold;

            textScore.SetText("Distance : "+ distanceInMeters.ToString("F2") + " meter");
            textScoreGameOver.SetText("Distance : "+ distanceInMeters.ToString("F2") + " meter");
            UpdatePowerUp();
        }
    }

    // Add your GameManager functionality here
    public void ControlUp(){
        if(lives > 0){
            player.GetComponent<PlayerController>().ApplyUpForce();
            lives--;
            UpdateLives();
        }
    }

    
    public void ControlDown(){
        if(isPowerActive){
            player.GetComponent<PlayerController>().ApplyDownForce();
            ResetPowerUp();
        }
    }

    private void UpdatePowerUp()
    {
        if(!isPowerActive)
        {
            distancePowerUp = Vector3.Distance(player.transform.position, lastDistancePoint);

            float distanceInMeters = distancePowerUp / distanceThreshold;
            float normalizedDistance = Mathf.Clamp01(distanceInMeters / maxDistancePower);
            float sliderValue = normalizedDistance * 10f * 0.1f;

            // Update the slider value
            sliderPower.value = sliderValue;

            if(sliderPower.value >= 1){
                isPowerActive = true;
            }
        }
    }

    private void ResetPowerUp(){
        sliderPower.value = 0;
        lastDistancePoint = player.transform.position;
        isPowerActive = false;
    }

    private void UpdateLives()
    {
        int iconsToDim = Mathf.Clamp(startingLives - lives, 0, startingLives);
        for (int i = 0; i < liveIcons.Count; i++)
        {
            if (i < iconsToDim)
            {
                liveIcons[i].color = new Color32(255,255,255,150);
            }
            else
            {
                liveIcons[i].color = new Color32(255,255,255,255);
            }
        }
    }

    public void AddLive(){
        if(lives < startingLives){
            lives++;
            UpdateLives();
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
        if(player != null){
            sliderPower.value = 0;
            lives = startingLives;
            UpdateLives();
            player.GetComponent<PlayerController>().UnFreezeControl();
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<PlayerController>().ApplyUpForce();
            playerUI.gameObject.SetActive(true);
            isGameStart = true;
            lastDistancePoint = firstPoint.transform.position;
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("Game Ended!");
        isGameStart = false;
        gameOverUI.SetActive(true);
        
    }

    public void PauseGame(){
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        Time.timeScale = 1;
    }

    public void ExitMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
