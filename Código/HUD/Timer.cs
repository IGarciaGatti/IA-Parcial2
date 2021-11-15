using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text mainText;
    [SerializeField] private Text secondaryText;
    private bool levelFinished;
    private float reverseTime = 0f;

    public delegate void TimerDelegate();
    public TimerDelegate OnTimeLimitReached;

    void Update()
    {
        Countdown();
        DisplayTime();
    }

    private void Countdown()
    {
        if (!levelFinished)
        {
            if (time > 0f)
            {
                time -= Time.deltaTime;
                reverseTime += Time.deltaTime;
            }
            else
            {
                time = 0f;
                OnTimeLimitReached();
                levelFinished = true;
            }
        }
        
    }

    private void DisplayTime()
    {
        if(time < 0f)
        {
            time = 0f;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = time % 1 * 1000;

        mainText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        secondaryText.text = string.Format("{0:000}", milliseconds);
    }

    public string TimeElapsed()
    {
        float minutes = Mathf.FloorToInt(reverseTime / 60);
        float seconds = Mathf.FloorToInt(reverseTime % 60);
        float milliseconds = reverseTime % 1 * 1000;

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public void LevelFinish(bool condition)
    {
        levelFinished = condition;
    }
}
