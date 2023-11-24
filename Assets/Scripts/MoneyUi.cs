using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUi : MonoBehaviour
{
    public TMP_Text moneyText;

    private void Update()
    {
        moneyText.text = " C" +PlayerStats.Money.ToString() ;
    }
}
