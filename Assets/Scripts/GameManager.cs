using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private bool gameEned = false;
    public waypointSpawner wayponitSpawn;
    public GameObject gameOverUi;
    public GameObject gameWinUi;
    public static bool gameWin = false;

    private void Start()
    {
        gameWinUi.SetActive(false);
        gameWin = false;
        wayponitSpawn.enabled = true;
    }

    void Update()
    {
        if (gameEned) return;

        if (PlayerStats.Live <= 0)
        {
            EndGame();
        }
        else if (gameWin == true){
            WinGame();
        }
    }
    public void EndGame()
    {
        gameEned = true;
        FindObjectOfType<SoundManager>().HalfDownSounded("Theam");
        FindObjectOfType<SoundManager>().PlaySounded("Defeat");
        gameOverUi.SetActive(true);
        wayponitSpawn.enabled = false;
        
    }

    public void TimePlus()
    {
        if(Time.timeScale == 2)
            Time.timeScale = 1;
        else if(Time.timeScale == 1)
            Time.timeScale = 2;
    }

    void WinGame()
    {
        gameEned = true;
        FindObjectOfType<SoundManager>().HalfDownSounded("Theam");
        FindObjectOfType<SoundManager>().PlaySounded("Victory");
        gameWinUi.SetActive(true);
    }
}
