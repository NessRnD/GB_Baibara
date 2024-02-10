using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent onDeathEvent;
    public UnityEvent<float> onTakeDamageEvent;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private bool isAlive;

    private void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        onTakeDamageEvent.Invoke(currentHealth);
        CheckIsAlive();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void DestroyOnDeath()
    {
        Destroy(gameObject);
    }

    public bool IsDeadCheker()
    {
        if (currentHealth > 0)
            return false;
        else
        {
            return true;
        }
            
    }

    private void CheckIsAlive()
    {
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            currentHealth = 0;
            onDeathEvent.Invoke();
        }
    }
}
