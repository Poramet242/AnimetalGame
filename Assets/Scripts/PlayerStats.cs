using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Rounds;
    public int startMoney = 99;

    public Slider moneySlider;
    public float MaxTime = 1f;
    private float CurrentTime = 0;
    public static bool canPlusTime;

    public static int Live;
    public int startLive = 3;
    private void Start()
    {
        Money = startMoney;
        Live = startLive;
        Rounds = 0;
    }

    private void Update()
    {
        GetTiming();
    }

    void GetTiming()
    {
        if (CurrentTime >= MaxTime && Money < 99f && canPlusTime)
        {
            CurrentTime = 0;
            Money += 1;
            canPlusTime = false;
        }
        else if(canPlusTime)
        {
            CurrentTime += Time.deltaTime;
        }
        

        moneySlider.maxValue = MaxTime;
        moneySlider.value = CurrentTime;
    }
}
