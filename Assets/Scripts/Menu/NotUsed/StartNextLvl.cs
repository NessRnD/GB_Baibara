using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNextLvl : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene(2);
    }
}
