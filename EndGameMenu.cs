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

    public void CallEndGameMenuOnDeath()
    {
        endGameText.text = "Game Over";
        StartCoroutine(DeathDelay());
    }

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

