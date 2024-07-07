using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour , IHealth
{
    [SerializeField] float totalHealth;
    [SerializeField] Image healthBar;

    private float currentHealth;
    private void Start()
    {
        currentHealth = totalHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage / totalHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);

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
