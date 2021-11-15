using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private string menuName;
    [SerializeField] private List<GameObject> hudObjectsToHide;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Text defeatText;
    [SerializeField] private Text victoryText;
    [SerializeField] private string victoryMessage;
    [SerializeField] private string defeatMessage;
    [SerializeField] private string timeLimitReachedMessage;
    private bool isGamePaused;

    public delegate void HUDManagerDelegate();
    public HUDManagerDelegate OnHUDEnter;
    public HUDManagerDelegate OnHUDExit;

    void Start()
    {
        timer.OnTimeLimitReached += TimeLimitReached;
        LevelStart(true);
    }

    void Update()
    {
        PauseInput();
    }
    
    private void LevelStart(bool condition)
    {
        if (condition)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        ManageHUDObjects(!condition);
        startPanel.SetActive(condition);
        if (!condition)
        {
            OnHUDExit();
        }
        isGamePaused = condition;
    }

    private void ManageHUDObjects(bool activate)
    {
        for (int i = 0; i < hudObjectsToHide.Count; i++)
        {
            hudObjectsToHide[i].SetActive(activate);
        }
    }

    private void Pause(bool condition, float timeScale)
    {
        Time.timeScale = timeScale;
        ManageHUDObjects(!condition);
        pausePanel.SetActive(condition);
        isGamePaused = condition;
        if (condition)
        {
            OnHUDEnter();
        }
        else
        {
            OnHUDExit();
        }
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Pause(false, 1f);
            }
            else
            {
                Pause(true, 0f);
            }
        }        
    }

    public void ExitPause()
    {
        if (isGamePaused)
        {
            Pause(false, 1f);
        }
    }

    public void Victory()
    {
        OnHUDEnter();
        timer.LevelFinish(true);
        ManageHUDObjects(false);  
        victoryText.text = victoryMessage + " " + timer.TimeElapsed();
        victoryPanel.SetActive(true);
    }

    public void Defeat()
    {
        OnHUDEnter();
        timer.LevelFinish(true);
        ManageHUDObjects(false);
        defeatText.text = defeatMessage;
        defeatPanel.SetActive(true);
    }

    public void TimeLimitReached()
    {
        OnHUDEnter();
        defeatText.text = timeLimitReachedMessage;
        defeatPanel.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuName);
    }
}
