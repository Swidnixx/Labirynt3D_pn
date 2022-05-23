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

    // Sounds
    public AudioSource audioSource;
    public AudioClip gamePausedClip;
    public AudioClip pickUpClip;
    public AudioClip loseClip;

    private int redKeys = 0;
    public int RedKeys { get { return redKeys; } set { redKeys = value; } }
    private int greenKeys = 0;
    public int GreenKeys { get { return greenKeys; } set { greenKeys = value; } }
    private int goldenKeys = 0;
    public int GoldenKeys { get { return goldenKeys; } set { goldenKeys = value; } }

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

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            PlayClip(gamePausedClip);
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
        PlayClip(loseClip);
        Debug.Log("Game ended");
    }

    #region Pickups Helper Methods
    public void AddPoints(int pointsToAdd)
    {
        PlayClip(pickUpClip);
        points += pointsToAdd;
    }

    public void AddTime(int timeToAdd)
    {
        PlayClip(pickUpClip);
        timeLeft += timeToAdd;
    }

    public void TimeFreeze(int time)
    {
        PlayClip(pickUpClip);
        CancelInvoke("StopperTick");
        InvokeRepeating("StopperTick", time, 1);
    }

    public void AddKey(KeyType type)
    {
        PlayClip(pickUpClip);
        switch (type)
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
