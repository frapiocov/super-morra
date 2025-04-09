using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePlayer : MonoBehaviour
{

    [Header("UI settings")]
    [SerializeField] GameObject GameOverPanel;

    private int PlayerLifes = 3;
    
    void Start()
    {
        
    }

    public void RemoveLife()
    {

    }

    public void AddLife()
    {

    }

    private void Die()
    {
        GameOverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
