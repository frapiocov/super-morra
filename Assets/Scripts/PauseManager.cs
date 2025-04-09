using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] GameObject PausePanel;

    private bool isPause = false;

    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Restart()
    {
        isPause = false;
        // ricarica scena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPause)
        {
            Pause();
        } else if (Input.GetKeyDown(KeyCode.P) && isPause)
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPause = true;

        PausePanel.SetActive(true);

        Time.timeScale = 0f;
        // Mantieni attivo EventSystem
        EventSystem.current.enabled = true;
    }
}
