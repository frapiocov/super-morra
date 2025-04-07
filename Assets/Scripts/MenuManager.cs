using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //[Header("UI Elements")]

    // ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Show the players scores
    public void ShowScores() { }

    // Play Game
    public void Play()
    {
        SceneManager.LoadScene("Liv1-Teglia");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Credits()
    {
        Application.OpenURL("https://frapiocov.github.io/");
    }
}
