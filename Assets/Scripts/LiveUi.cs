using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveUi : MonoBehaviour
{
    public TMP_Text liveText;
    private void Update()
    {
         liveText.text = "LIFE " +PlayerStats.Live.ToString()+"/3";
    }
}
