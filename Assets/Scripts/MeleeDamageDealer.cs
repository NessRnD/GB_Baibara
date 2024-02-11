using UnityEngine;

public class MeleeDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string objectTag;
    private Health objectHealth;

    private void OnTriggerEnter(Collider myCollider)
    {
        if (myCollider.tag == objectTag)
        {
            objectHealth = myCollider.GetComponent<Health>();
            objectHealth.TakeDamage(damage);
        }
    }
}
