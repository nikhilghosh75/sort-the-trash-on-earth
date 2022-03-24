using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<ItemData> { }

public class GameController : MonoBehaviour
{
    public ObjectSpawner spawner;
    public GameUIManager manager;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;

    public ItemEvent OnRightDropped;
    public ItemEvent OnWrongDropped;

    [HideInInspector]
    public bool isPaused;

    [HideInInspector]
    public int lives = 5;

    int startLives = 5;

    [HideInInspector]
    public int score = 0;

    public static GameController controller;

    void Awake()
    {
        controller = this;
    }

    public void StartGame()
    {
        isPaused = false;
        lives = startLives;
        score = 0;

        PlayerHistory.Reset();
        OnGameStart.Invoke();
        spawner.StartGame();
        manager.StartGame();
    }
    
    public void OnItemDropped(bool correctCan, ItemData itemData)
    {
        if(correctCan)
        {
            OnRightDropped.Invoke(itemData);
            PlayerHistory.OnObjectSorted(itemData);
        }
        else
        {
            OnWrongDropped.Invoke(itemData);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
    }

    public void UnpauseGame()
    {
        isPaused = false;
    }

    public void TogglePaused()
    {
        isPaused = !isPaused;
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
    }

    public void LoseLife()
    {
        lives--;
        if(lives <= 0)
        {
            // OnGameEnd.Invoke();
        }
    }

    public void EndGame()
    {
        OnGameEnd.Invoke();
    }

    public void GainLife()
    {
        lives++;
    }
}
