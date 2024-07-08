using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] float timeCooldown;
    private void Update()
    {
        if (timeCooldown > 0f)
        {
            timeCooldown -= Time.deltaTime;
        }
        else
        {
            timeCooldown = 5f;

            GameObject bulletObj = Instantiate(bulletPrefab);

            bulletObj.transform.position = bulletHolder.transform.position;

            bulletObj.transform.forward = transform.forward;
        }
    }
}
