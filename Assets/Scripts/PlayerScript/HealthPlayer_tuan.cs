using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer_tuan : MonoBehaviour,IHealth_tuan
{
    private int maxHealth = 200;
    private int health;
    public Slider healthSileder;
    public GameObject gameOver;


    private void Start()
    {
        Health = MaxHealth;
        healthSileder.value = MaxHealth / MaxHealth;
    }

    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, MaxHealth); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        healthSileder.value -= amount / MaxHealth;
        if (Health <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        Health += amount;
        healthSileder.value += amount / MaxHealth;
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);
    }
}
