using UnityEngine;

public class BulletController_Tuan : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private string targetTag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            IHealth_tuan healthCollider = other.GetComponentInParent<IHealth_tuan>();
            if (healthCollider != null)
            {
                healthCollider.TakeDamage(50);
                SoundManager_tuan.instance.PlayAudio("Fire2");
                Debug.Log(other.name + " took 50 damage");
            }
            gameObject.SetActive(false);
        }
    }
}

