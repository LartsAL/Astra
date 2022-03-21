using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenCaller : MonoBehaviour
{
    public void GameOver()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
