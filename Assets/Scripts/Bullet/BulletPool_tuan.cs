using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool_tuan : MonoBehaviour
{
    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();
    }

    public void CreateBullet(int amountBullet, GameObject prefabBullet)
    {
        for (int i = 0; i < amountBullet; i++)
        {
            GameObject bullet = Instantiate(prefabBullet);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }

    public GameObject GetBullet(Transform spawnPoint)
    {
        foreach (var bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = spawnPoint.position;
                bullet.transform.rotation = spawnPoint.rotation;
                bullet.SetActive(true);

                Renderer bulletRenderer = bullet.GetComponent<Renderer>();
                if (bulletRenderer != null)
                {
                    bulletRenderer.enabled = true;
                }

                Debug.Log($"Bullet spawned at {spawnPoint.position} with rotation {spawnPoint.rotation}");
                StartCoroutine(TimeLifeOfBullet(bullet));
                return bullet;
            }
        }

        Debug.LogWarning("No inactive bullets available in the pool.");
        return null;
    }

    IEnumerator TimeLifeOfBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(5f);
        bullet.SetActive(false);
    }
}
