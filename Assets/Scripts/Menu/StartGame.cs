using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    /// <summary>
    /// Начало новой игры (кнопка)
    /// </summary>
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
