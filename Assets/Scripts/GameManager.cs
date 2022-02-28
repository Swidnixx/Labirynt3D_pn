using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;

    // Game Manager Settings
    [SerializeField] int timeLeft = 100;

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

    void StopperTick()
    {
        timeLeft--;
        print("Time left: " + timeLeft);
    }
}
