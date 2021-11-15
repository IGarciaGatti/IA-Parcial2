using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string firstLevelName;

    public void Play()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void ChangeWindowMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
