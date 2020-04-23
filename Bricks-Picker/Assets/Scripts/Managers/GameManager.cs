using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("MANAGERS")]
    public PlayerManager PlayerManager;
    public ObstacleManager ObstacleManager;
    public StackManager StackManager;
    [Header("CONTROLLERS")]
    public TouchController TouchController;
    public SuperPowerController SuperPowerController;
    public CongratWordsController CongratWordsController;
    public ColourController ColourController;
    public ScoreController ScoreController;
    public CoinController CoinController;
    public TimeController TimeController;

    public CameraMovement CameraMovement;

    public GameStates GameState;
    public bool IsGameStarted;

    public event Action OnGameStarted;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
            Destroy(this);
    }

    private void Start()
    {
        IsGameStarted = false;
    }

    private void Update()
    {
        if (GameState != GameStates.GameOnGoing)
            return;

        if (IsGameStarted)
        {
            OnGameStarted?.Invoke();
            IsGameStarted = false;
        }
    }
}