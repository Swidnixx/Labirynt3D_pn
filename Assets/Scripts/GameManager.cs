using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Game Manager Settings
    [SerializeField] int timeLeft = 100;

    // Game State
    private bool gamePaused;
    private bool gameEnded;

    // Statistics
    private int points = 0;

    // Sounds
    public AudioSource audioSource;
    public AudioClip gamePausedClip;
    public AudioClip pickUpClip;
    public AudioClip loseClip;
    public AudioClip winClip;

    // UI
    public Text diamondsText;
    public Text redKeysText;
    public Text greenKeysText;
    public Text goldKeysText;
    public Text timeText;

    public GameObject endGamePanel;
    public Text infoText;
    public Text endGameText;


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

    void Update()
    {
        if(gameEnded)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }

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

   

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void StopperTick()
    {
        timeLeft--;
        timeText.text = timeLeft.ToString();

        if(timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }
    }
    public void WinGame()
    {
        CancelInvoke(nameof(StopperTick));
        PlayClip(winClip);

        endGamePanel.SetActive(true);
        endGameText.text = "You Won!";
        gameEnded = true;
    }

    void EndGame()
    {
        CancelInvoke(nameof(StopperTick));
        PlayClip(loseClip);

        endGamePanel.SetActive(true);
        gameEnded = true;
    }

    #region Pickups Helper Methods
    public void AddPoints(int pointsToAdd)
    {
        PlayClip(pickUpClip);
        points += pointsToAdd;
        diamondsText.text = points.ToString();
    }

    public void AddTime(int timeToAdd)
    {
        PlayClip(pickUpClip);
        timeLeft += timeToAdd;
        timeText.text = timeLeft.ToString();
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
                redKeysText.text = redKeys.ToString();
                break;

            case KeyType.Green:
                greenKeys++;
                greenKeysText.text = greenKeys.ToString();
                break;

            case KeyType.Gold:
                goldenKeys++;
                goldKeysText.text = goldenKeys.ToString();
                break;
        }
    }
    #endregion
}
