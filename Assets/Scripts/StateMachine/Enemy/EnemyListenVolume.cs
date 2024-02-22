using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListenVolume : MonoBehaviour
{
    public EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object Detected -> {other.gameObject.name}");
    }
}
