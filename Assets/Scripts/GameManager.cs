using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    //SFX audio Manager
    AudioManager audioManager;
    
    public static GameManager instance;

    // Defining the stats of the game
    // Start with Caps/Upper case when naming Enums
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    // Stores current game state
    public GameState currentState;

    // Stores previous game state
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject levelUpScreen;

    [Header("Paused Stat Displays")]
    public TMP_Text pausedHealthDisplay;
    public TMP_Text pausedRecoveryDisplay;
    public TMP_Text pausedMoveSpeedDisplay;
    public TMP_Text pausedMightDisplay;
    public TMP_Text pausedProjectileSpeedDisplay;
    public TMP_Text pausedMagnetDisplay;

    [Header("Current Stat Displays")]
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentMightDisplay;
    public TMP_Text currentProjectileSpeedDisplay;
    public TMP_Text currentMagnetDisplay;

    [Header("Results Screen Displays")]
    public TMP_Text levelReachedDisplay;
    public TMP_Text timeSurvivedDisplay;





    [Header("Stopwatch")]
    public float timeLimit; // Time in seconds
    float stopwatchTime;
    public TMP_Text stopwatchDisplay;



    public bool isGameOver = false;

    // Flag to check if player is upgrading
    public bool choosingUpgrade;

    // Reference player's game object
    public GameObject playerObject;


    void Awake()
    {

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        // Warning to see if there is another singleton of this kind in game
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "Deleted");
        }


        DisableScreens();
    }

    void Update()
    {

        // TestSwitchState();

        // Define behaviour for each state
        // Below, we will enter code for each gameplay state
        // The code will be executed when the current gameplay state matches

        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatch();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:

                if(!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; // Stop game completely
                    Debug.Log("Game over!");
                    DisplayResults();
                }
                break;

            case GameState.LevelUp:

                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f; // Stop game completely
                    Debug.Log("Choosing Upgrade!");
                    levelUpScreen.SetActive(true);
                }
                break;
            
            // error handling block for troubleshooting, should game reach an invalid state
            default:
                Debug.LogWarning("State non-existent");
                break;

        }
    }

    // Method to change state of game, instead of relying on =
    // Accepts arguments of type GameState
    // Allows us to track where and when state changes are happening
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {

            pausedHealthDisplay.text = currentHealthDisplay.text;
            pausedRecoveryDisplay.text = currentRecoveryDisplay.text;
            pausedMoveSpeedDisplay.text = currentMoveSpeedDisplay.text;
            pausedMightDisplay.text = currentMightDisplay.text;
            pausedProjectileSpeedDisplay.text = currentProjectileSpeedDisplay.text;
            pausedMagnetDisplay.text = currentMagnetDisplay.text;

            audioManager.PlaySound(audioManager.pause);
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; // Stops game
            pauseScreen.SetActive(true);
            Debug.Log("Game paused");
        }
        
    }


    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState); // Reverts from pause back to EXACT point when game is stopped
            Time.timeScale = 1f; // Resumes game
            pauseScreen.SetActive(false);
            Debug.Log("Game paused");
        }
    }


    // Define the method to check for pause and resume input
    void CheckForPauseAndResume()
    {
        // Checks for Esc Key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    public void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if(stopwatchTime >= timeLimit)
        {
            // Call the Defeated method from PlayerStats
            // Instead of directly calling GameOver
            playerObject.SendMessage("Defeated");
        }
    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime/60);
        int seconds = Mathf.FloorToInt(stopwatchTime%60);   // Modulo obtains the seconds remainder

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        audioManager.PlaySound(audioManager.lvlUp);
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        audioManager.PlaySound(audioManager.clickLvlUp);
        choosingUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
    
    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }
}
