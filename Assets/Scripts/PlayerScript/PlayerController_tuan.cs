using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_tuan : MonoBehaviour
{
    private HealthPlayer_tuan healthPlayer;
    private MovementPlayer_tuan movementPlayer;
    private AttackPlayer_tuan attackPlayer;
    private void Start()
    {
        healthPlayer = GetComponent<HealthPlayer_tuan>();

        movementPlayer = GetComponent<MovementPlayer_tuan>();

        attackPlayer = GetComponent<AttackPlayer_tuan>();
    }
    private void Update()
    {
        attackPlayer.FindEnemy();
    }
}
