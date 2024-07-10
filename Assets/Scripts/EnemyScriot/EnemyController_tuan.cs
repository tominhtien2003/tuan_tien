using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController_tuan : MonoBehaviour,IHealth_tuan
{
    private int maxHealth = 200;
    private int health;
    private Rigidbody rb;
    public Transform center;
    public Transform barrel;

    private NavMeshAgent agent;
    public PlayerController_tuan player;

    public GameObject bulletPrefab;
    public Slider healthSlider;
    public Transform spawnPoint;
    public Camera mainCamera;
    public Vector3 offset;
    private bool canAttack = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        InvokeRepeating(nameof(Move), 0, 0.2f);
        healthSlider.value = 1f;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        FindPlayer();
        HealthPosition();
    }

    private void HealthPosition()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position) + offset;
        healthSlider.transform.position = screenPosition;
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
        Destroy(gameObject);
        healthSlider.gameObject.SetActive(false);
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
}
