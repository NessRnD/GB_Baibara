using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Класс отвечающий за здоровье как игрока так и врагов
/// </summary>
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

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damage"></param> float damage - количество наносимого урона
    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " " + "taked Damage:" + damage + " currentHealth=" + currentHealth);
        currentHealth -= damage;
        onTakeDamageEvent.Invoke(currentHealth);
        CheckIsAlive();
    }

    /// <summary>
    /// Метод запроса оставшегося здоровья
    /// </summary>
    public float GetHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Метод уничтожения объекта после смерти
    /// </summary>
    public void DestroyOnDeath()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Внешняя проверка на живность
    /// </summary>
    public bool IsDeadCheker()
    {
        if (currentHealth > 0)
            return false;
        else
        {
            return true;
        }
            
    }

    /// <summary>
    /// Внутренняя проверка на живность
    /// </summary>
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
