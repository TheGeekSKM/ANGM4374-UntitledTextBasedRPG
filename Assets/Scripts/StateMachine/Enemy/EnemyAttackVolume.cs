using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackVolume : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<PlayerHealth>();
        if (!health) return;

        enemyController.StartEnemyAttack(health);
    }

    private void OnTriggerExit(Collider other)
    {
        var health = other.GetComponent<PlayerHealth>();
        if (!health) return;

        enemyController.EnemyIdle();
    }
}
