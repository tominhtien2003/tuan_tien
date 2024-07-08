﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController_tuan : MonoBehaviour
{
    private int maxHealth = 200;
    private int health;
    private Rigidbody rb;
    public Transform center;
    public Transform barrel;

    private NavMeshAgent agent;
    public PlayerController_tuan player;

    public GameObject bulletPrefab;
    public Transform spawnPoint;

    private bool canAttack = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Health = MaxHealth;
        InvokeRepeating(nameof(Move), 0, 0.2f);
    }

    private void Update()
    {
        FindPlayer();
    }

    private void Move()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 20f)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
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
        if (Health <= 0)
        {
            Dead();
        }
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Dead()
    {
        Debug.Log("Killed enemy");
        gameObject.SetActive(false);
    }

    private IEnumerator RotateBarrelAndAttack(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        while (barrel.rotation != targetRotation)
        {
            barrel.rotation = Quaternion.RotateTowards(barrel.rotation, targetRotation, Time.deltaTime * 1f);
            yield return null;
        }
        Attack();
    }

    private void FindPlayer()
    {
        float rayLength = 100f;
        int numRays = 18;

        for (int i = 0; i < numRays; i++)
        {
            float angle = i * (360f / numRays);
            Vector3 rayDirection = Quaternion.Euler(0f, angle, 0f) * center.forward;

            RaycastHit hit;
            if (Physics.Raycast(center.position, rayDirection, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Raycast hit player: " + hit.collider.name);
                    Debug.DrawRay(center.position, rayDirection * rayLength, Color.red);

                    StartCoroutine(RotateBarrelAndAttack(rayDirection));
                }
            }
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(Fire());
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator Fire()
    {
        barrel.transform.position -= barrel.transform.forward * 0.5f;
        yield return new WaitForSeconds(0.05f);
        barrel.transform.position += barrel.transform.forward * 0.5f;
        GameController_tuan.instance.GetBullet(spawnPoint, bulletPrefab);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(6f);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        }
    }
}