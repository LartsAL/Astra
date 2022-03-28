using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenCaller : MonoBehaviour
{
    public GameSaver GS;
    public void GameOver()
    {
        if (GS != null)
        {
            GS.SaveGame(GS.currentData);
        }
        SceneManager.LoadScene("DeathScreen");
    }
}
