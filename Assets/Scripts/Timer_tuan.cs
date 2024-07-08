using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer_tuan : MonoBehaviour
{
    private float elapsedTime;
    private int seconds;
    private int minutes;
    public TextMeshProUGUI textTimer;

    private float countdownElapsedTime;
    private int countdownSeconds;
    private int countdownTime = 6;
    public TextMeshProUGUI textTimeLoadBullet;
    public Button fire;
    public Color colorFireButton;

    private bool isFired;

    void Start()
    {
        elapsedTime = 0f;
        seconds = 0;
        minutes = 0;

        countdownElapsedTime = 0f;
        countdownSeconds = countdownTime;
    }

    void Update()
    {
        TimePlay();
        TimeLoadBullet();
    }

    private void TimePlay()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            seconds++;
            elapsedTime = 0f;

            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }

            textTimer.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
    }

    private void TimeLoadBullet()
    {
        if (isFired)
        {
            fire.image.color = colorFireButton;
            countdownElapsedTime += Time.deltaTime;
            if (countdownElapsedTime >= 1f)
            {
                countdownSeconds--;
                countdownElapsedTime = 0f;

                if (countdownSeconds <= 0)
                {
                    countdownSeconds = countdownTime;
                    textTimeLoadBullet.text = "";
                    isFired = false;
                    fire.image.color = Color.white;
                }
                else
                {
                    textTimeLoadBullet.text = countdownSeconds.ToString();
                }
            }
        }
    }

    public void StartCountdown()
    {
        isFired = true; 
    }
}
