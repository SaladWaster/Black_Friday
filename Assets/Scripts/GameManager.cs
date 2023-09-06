using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Defining the stats of the game
    // Start with Caps/Upper case when naming Enums
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    // Stores current game state
    public GameState currentState;

    // Stores previous game state
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;

    void Awake()
    {
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
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
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
    }

    // // Test function
    // // An enum is a set of named integer constants
    // // thus, incrementing/decrementing works
    // void TestSwitchState()
    // {
    //     if(Input.GetKeyDown(KeyCode.E))
    //     {
    //         currentState++;
    //     }
    //     else if(Input.GetKeyDown(KeyCode.Q))
    //     {
    //         currentState--;
    //     }
    // }
}
