using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWin : MonoBehaviour
{
    public TMP_Text roundText;
    public ScenesFader ScenesFader; 
    public void Retry()
    {
        ScenesFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        Debug.Log("Go to Menu");
    }
}