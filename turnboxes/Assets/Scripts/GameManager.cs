using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentLevel;

    private void Awake()
    {
        Instance = this;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        Debug.Log(currentLevel);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("IsPlaying", 1);
    }

    public void LoadStart()
    {
        PlayerPrefs.SetInt("IsPlaying", 0);
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("IsPlaying", 0);
    }
}

