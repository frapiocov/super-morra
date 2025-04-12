using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI settings")]
    [SerializeField] public GameObject GameOverPanel;
    [SerializeField] public GameObject FinishPanel;
    [SerializeField] public TextMeshProUGUI CollectText;
    
    private int progressAmount;

    void Start()
    {
        progressAmount = 0;
    }

    public void IncreaseCollection()
    {
        progressAmount += 1;
        CollectText.text = "raccolti: " + progressAmount + " / 20";

        if(progressAmount == 20)
        {
            // Level complete
            Time.timeScale = 0f;
            // EventSystem enabled
            EventSystem.current.enabled = true;
            FinishPanel.SetActive(true);
        }
    }

    public void PlayerDeath()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.enabled = true;
    }
}
