using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent offPauseMenuEvent;
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioSourceObject;
    private AudioSource audioSource;
    private bool isPaused = false;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        audioSource = audioSourceObject.GetComponent<AudioSource>();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        audioSource.Pause();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        offPauseMenuEvent.Invoke();
        isPaused = false;
        pauseMenu.SetActive(false);
        audioSource.Play();
        Time.timeScale = 1f;
    }

    public void PauseTumbler()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
