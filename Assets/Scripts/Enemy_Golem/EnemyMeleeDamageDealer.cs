using UnityEngine;

public class EnemyMeleeDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string playerTag;
    private Health playerHealth;

    private void OnTriggerEnter(Collider myCollider)
    {
        if (myCollider.tag == playerTag)
        {
            playerHealth = myCollider.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
        }
    }

}
