using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] IntSO currentHealth;
    [SerializeField] AttributeData attributes;
    Coroutine continuousDamageRoutine;

    private void Start() {
        currentHealth.SetValue(attributes.Vitality * 10);
    }

    public void TakeDamage(int damage)
    {
        currentHealth.Add(-damage);
        if (currentHealth.value <= 0)
        {
            Die();
        }
    }

    public void TakeContinuousDamage(int damage)
    {
        currentHealth.Add(-damage);

        if (continuousDamageRoutine == null)
        {
            continuousDamageRoutine = StartCoroutine(ContinuousDamage(damage, 3));
        }
        else
        {
            StopCoroutine(continuousDamageRoutine);
            continuousDamageRoutine = StartCoroutine(ContinuousDamage(damage, 3));
        }
    }

    IEnumerator ContinuousDamage(int damage, float timeBetweenDamage)
    {
        while (true)
        {
            currentHealth.Add(-damage);
            if (currentHealth.value <= 0)
            {
                Die();
            }
            yield return new WaitForSeconds(timeBetweenDamage);
        }
    }

    public void StopTakingDamage()
    {
        if (continuousDamageRoutine != null) StopCoroutine(continuousDamageRoutine);
    }

    void Die()
    {
        Debug.Log("TODO: Die() method in PlayerHealth.cs");
    }



}
