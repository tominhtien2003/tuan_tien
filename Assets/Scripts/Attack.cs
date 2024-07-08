using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] Transform gunBarrel;
    [SerializeField] float timeCooldown;
    private void Update()
    {
        if (timeCooldown > 0f)
        {
            timeCooldown -= Time.deltaTime;
        }   
    }
    public void Shoot()
    {
        if (timeCooldown > 0f)
        {
            return;
        }
        timeCooldown = 2f;

        GameObject bulletObj = Instantiate(bulletPrefab);

        bulletObj.transform.position = bulletHolder.transform.position;

        bulletObj.transform.forward = gunBarrel.forward;
    }
}
