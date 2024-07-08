using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float moveSpeed;

    private float timeLife = 5f;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (timeLife <= 0)
        {
            Destroy(gameObject);
        }
        timeLife -= Time.deltaTime;

        Vector3 forwardMove = transform.forward * moveSpeed;

        rb.velocity = new Vector3(forwardMove.x, rb.velocity.y, forwardMove.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.layer == 6)
        {
            obj.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        if (obj.tag == "Player")
        {
            obj.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
        Debug.Log(collision.gameObject);
    }
}
