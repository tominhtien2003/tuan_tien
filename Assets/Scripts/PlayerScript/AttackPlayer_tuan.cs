using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer_tuan : MonoBehaviour, IAttack_tuan
{
    public Transform barrel;
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public GameObject center;
    public LayerMask hitlayers;
    private bool canAttack = true;

    public void Attack()
    {
        if (canAttack)
        {
            GameController_tuan.instance.GetBullet(spawnPoint, bulletPrefab);
            StartCoroutine(Fire());
            SoundManager_tuan.instance.PlayAudio("Fire");
            canAttack = false;
            StartCoroutine(ResetAttackCooldown());
        }
    }

    IEnumerator Fire()
    {
        barrel.transform.position -= barrel.transform.forward * 0.5f;
        yield return new WaitForSeconds(0.05f);
        barrel.transform.position += barrel.transform.forward * 0.5f;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(6f);
        canAttack = true;
    }

    public void FindEnemy()
    {
        Debug.DrawRay(barrel.position, barrel.forward * 100f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(barrel.position, barrel.forward, out hit, 100f, hitlayers))
        {
            Collider parentCollider = hit.collider.transform.parent.GetComponent<Collider>();
            if (parentCollider != null)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(parentCollider.bounds.center);
                center.transform.position = screenPos;
            }
        }
    }

}