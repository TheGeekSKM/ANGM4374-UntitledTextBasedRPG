using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/ConsumableItemData")]
public class ConsumableItemData : ItemData
{
    public int HealthRestored;
    public int StaminaRestored;

    public override void UseItem()
    {
        Debug.Log("Using Consumable Item");
    }
}
