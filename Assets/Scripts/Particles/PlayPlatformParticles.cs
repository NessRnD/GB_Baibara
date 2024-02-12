using UnityEngine;

public class PlayPlatformParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireCircle;
    [SerializeField] private ParticleSystem splash;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            splash.Play();
            fireCircle.Play();
        }
    }

    private void OnDisable()
    {
        splash.Stop();
        fireCircle.Stop();
    }

    public void PlayParticles()
    {
        splash.Play();
        fireCircle.Play();
    }
}