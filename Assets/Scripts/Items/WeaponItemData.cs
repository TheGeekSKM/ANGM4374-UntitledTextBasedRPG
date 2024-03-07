using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemData : MonoBehaviour
{
    [Header("Weapon Item Data")]
    public float Damage;
    public float AttackRange;

    public void UseItem()
    {
        Debug.Log("WeaponItemData: UseItem");
        GameController.Instance.Attack(Damage, AttackRange);
    }
}
