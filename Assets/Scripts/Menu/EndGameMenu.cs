using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private Text endGameText;
    
    private void Awake()
    {
        endGameMenu.SetActive(false);
    }
    
    /// <summary>
    /// Вызов экрана поражения при смерти игрока.
    /// </summary>
    public void CallEndGameMenuOnDeath()
    {
        endGameText.text = "Game Over";
        StartCoroutine(DeathDelay());
    }
    
    /// <summary>
    /// Корутина задержки вызова экрана поражения
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathDelay() 
    { 
        yield return new WaitForSeconds(1);
        endGameMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        StopCoroutine(DeathDelay());
    }
}