using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject    
{
    public List<ItemData> Items;
    public int MaxItems = 10;

    public bool AddItem(ItemData item)
    {
        if (Items.Count > MaxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        Items.Add(item);
        return true;
    }

    public bool AddUniqueItem(ItemData item)
    {
        if (Items.Count > MaxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        if (!Items.Contains(item))
        {
            Items.Add(item);
            return true;
        }

        return false;
    }

    public void RemoveItem(ItemData item)
    {
        Items.Remove(item);
    }
}
