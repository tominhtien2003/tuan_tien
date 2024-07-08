using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer_tuan : MonoBehaviour, IHealth_tuan
{
    private int maxHealth = 400;
    private int health;
    public Slider healthSlider;
    public GameObject gameOver;

    private void Start()
    {
        health = maxHealth;
        healthSlider.value = 1f;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthSlider.value = (float)health / maxHealth; 
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth; 
        }
        healthSlider.value = (float)health / maxHealth;
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);
    }
}
