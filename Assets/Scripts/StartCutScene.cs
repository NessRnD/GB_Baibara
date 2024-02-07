using UnityEngine;
using UnityEngine.Playables;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.Play();
        }
    }
}
