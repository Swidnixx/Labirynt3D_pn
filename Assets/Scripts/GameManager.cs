using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Game Manager Settings
    [SerializeField] int timeLeft = 100;
    // Game State
    private bool gamePaused;

    // Statistics
    private int points = 0;
    private int redKeys = 0;
    private int greenKeys = 0;
    private int goldenKeys = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple GameManagers in the Scene");
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(StopperTick), 3, 1);
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(gamePaused)
            {
                //unpause
                gamePaused = false;
                Time.timeScale = 1;
                Debug.Log("Game resumed");
            }
            else
            {
                //pause
                gamePaused = true;
                Time.timeScale = 0;
                Debug.Log("Game paused");
            }
        }
    }

    void StopperTick()
    {
        timeLeft--;
        //print("Time left: " + timeLeft);

        if(timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }
    }

    void EndGame()
    {
        CancelInvoke(nameof(StopperTick));
        Debug.Log("Game ended");
    }

    #region Pickups Helper Methods
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public void AddTime(int timeToAdd)
    {
        timeLeft += timeToAdd;
    }

    public void TimeFreeze(int time)
    {
        CancelInvoke("StopperTick");
        InvokeRepeating("StopperTick", time, 1);
    }

    public void AddKey(KeyType type)
    {
        switch(type)
        {
            case KeyType.Red:
                redKeys++;
                break;

            case KeyType.Green:
                greenKeys++;
                break;

            case KeyType.Gold:
                goldenKeys++;
                break;
        }
    }
    #endregion
}
