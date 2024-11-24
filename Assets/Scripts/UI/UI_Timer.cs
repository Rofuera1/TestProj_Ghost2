using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    private System.DateTime timeFrom;
    private System.TimeSpan timer;

    private void OnEnable()
    {
        timeFrom = System.DateTime.Now;
    }

    private void Update()
    {
        timer = System.DateTime.Now - timeFrom;
        TimerText.text = convertIntToStringPretty(timer.Minutes) + ":" + convertIntToStringPretty(timer.Seconds) + ":" + convertIntToStringPretty(timer.Milliseconds);
    }

    private string convertIntToStringPretty(int a)
    {
        return (a < 10 ? "0" : "") + a.ToString();
    }
}
