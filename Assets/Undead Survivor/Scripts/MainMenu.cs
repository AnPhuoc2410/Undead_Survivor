using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(int id)
    {
        Time.timeScale = 1f;
        GameData.instance.playerIndex = id;
        SceneManager.LoadScene("Main_Scene");
    }
}
