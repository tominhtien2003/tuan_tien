using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IHealth
{
    [SerializeField] Image healthBar;

    private float currentHealth;
    private void Start()
    {
        currentHealth = 1;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, 1);

        if (currentHealth >= 0)
        {
            healthBar.fillAmount = currentHealth;

            if (currentHealth == 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
