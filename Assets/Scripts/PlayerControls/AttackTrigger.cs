using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public float Damage;
    public float Range;
    public ContactFilter2D ContactFilter;
    
    public void SetAttack(float damage, float range)
    {
        Damage = damage;
        Range = range;
        transform.localScale = new Vector3(Range, Range, Range);
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TODO: AttackTrigger: OnTriggerEnter");
    }

}
