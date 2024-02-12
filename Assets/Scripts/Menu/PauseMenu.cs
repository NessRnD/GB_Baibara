using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent offPauseMenuEvent;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject audioSourceObject;
    private AudioSource audioSource;
    private bool isPaused;

    private void Awake()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        audioSource = audioSourceObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Включение экрана паузы (с кнопки или UI)
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        audioSource.Pause();
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Отключение экрана паузы и продолжение игры
    /// offPauseMenuEvent - это событие, реализованное с помощью библиотеки UnityEngine.Events
    /// отвечает за включение управления
    /// </summary>
    public void Resume()
    {
        offPauseMenuEvent.Invoke();
        isPaused = false;
        pauseMenu.SetActive(false);
        audioSource.Play();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Включение/выключения паузы с помощью UI
    /// </summary>
    public void PauseTumbler()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    /// <summary>
    /// Выход в главное меню (кнопка) 
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    /// <summary>
    /// Рестарт уровня (кнопка)
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
