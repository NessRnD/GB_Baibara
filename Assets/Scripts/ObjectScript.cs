using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField] private float force = 500f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void ChangeScale()
    {
        Vector3 sizeAddition = new Vector3(transform.localScale.x, transform.localScale.y + 2, transform.localScale.z);
        transform.localScale = sizeAddition;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * force);
    }

    public void ObjDestroy()
    {
        Destroy(gameObject);
    }
}
