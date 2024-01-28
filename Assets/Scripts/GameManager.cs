using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerUI;
    public GameObject gameOverUI;
    public Transform firstPoint;
    
    public TextMeshProUGUI textScoreGameOver;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLive;
    public int startingLives = 3; // Number of starting lives
    private int lives; // Current lives count
    private static GameManager instance;
    private bool isGameStart;

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

    private void Start(){
        isGameStart = false;
        if(player != null){
            player.GetComponent<PlayerController>().ResetRotation();
            player.transform.position = new Vector3(35,6,0);
            player.GetComponent<PlayerController>().FreezeControl();
            player.GetComponent<PlayerController>().enabled = false;
            playerUI.gameObject.SetActive(false);
            gameOverUI.SetActive(false);
        }else{
            player = FindObjectOfType<PlayerController>().gameObject;
            player.transform.position = new Vector3(35,6,0);
            player.GetComponent<PlayerController>().ResetRotation();
            player.GetComponent<PlayerController>().FreezeControl();
            player.GetComponent<PlayerController>().enabled = false;
            playerUI.gameObject.SetActive(false);
            gameOverUI.SetActive(false);
        }
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
        if(player != null){

        // Calculate the distance between the player and the first point
        float distance = Vector3.Distance(player.transform.position, firstPoint.position);

        // Convert the distance to meters (assuming 1 Unity unit = 1 meter)
        float distanceInMeters = distance / 10;
        textScore.SetText("Distance : "+ distanceInMeters.ToString("F2") + " meter");
        textScoreGameOver.SetText("Distance : "+ distanceInMeters.ToString("F2") + " meter");
        }
    }

    // Add your GameManager functionality here
    public void ControlUp(){
        if(lives > 0){
            player.GetComponent<PlayerController>().ApplyUpForce();
            lives--;
            textLive.SetText("Lives : "+lives);
        }
    }

    public void AddLive(){
        lives++;
        textLive.SetText("Lives : "+lives);
    }

    public void ControlDown(){
        player.GetComponent<PlayerController>().ApplyDownForce();
    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
        if(player != null){
            lives = startingLives;
            textLive.SetText("Lives : "+lives);
            player.GetComponent<PlayerController>().UnFreezeControl();
            player.GetComponent<PlayerController>().enabled = true;
            ControlUp();
            playerUI.gameObject.SetActive(true);
            isGameStart = true;
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
