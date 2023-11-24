using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text roundText;
    public ScenesFader ScenesFader;
    private void OnEnable()
    {
        //roundText.text = PlayerStats.Rounds.ToString();
    }
    public void Retry()
    {
        waypointSpawner.instance.EnemiesAlibe = 0;
        ScenesFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Debug.Log("Go to Menu");
    }
}
