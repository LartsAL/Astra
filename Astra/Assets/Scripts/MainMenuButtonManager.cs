using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    public GameObject worldSlots;
    public GameSaver GS;
    public WorldNumberContainer WNC;
    public void Play()
    {
        //SceneManager.LoadScene("Main");
        worldSlots.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        worldSlots.SetActive(false);
        Application.Quit();
    }
    public void StartInAWorld(int number)
    {
        WNC.worldNumber = number;
        SceneManager.LoadScene("Main");
    }
}
