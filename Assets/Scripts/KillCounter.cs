using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    private int killCount = 0;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        UpdateKillCountText();
    }

    public void IncrementKillCount()
    {
        killCount++;
        UpdateKillCountText();
    }

    private void UpdateKillCountText()
    {
        textMeshPro.text = "" + killCount.ToString();
    }
}