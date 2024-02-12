using UnityEngine;


public class ExitGame : MonoBehaviour
{
    /// <summary>
    /// Общий скрипт для кнопок выхода из игры
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
