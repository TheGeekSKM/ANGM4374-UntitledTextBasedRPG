using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] IntSO currentHealth;

    private void Start() {
        currentHealth.SetValue(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth.Add(-damage);
        if (currentHealth.value <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("TODO: Die() method in PlayerHealth.cs");
    }



}
