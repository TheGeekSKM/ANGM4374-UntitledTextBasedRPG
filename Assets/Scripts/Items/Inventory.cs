using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject    
{
    public List<ItemData> Items;
    public int MaxItems = 10;

    public UnityEvent<ItemData> OnItemAdded;
    public UnityEvent<ItemData> OnItemRemoved;

    public bool AddItem(ItemData item)
    {
        if (Items.Count > MaxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        Items.Add(item);
        OnItemAdded?.Invoke(item);
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
            OnItemAdded?.Invoke(item);
            return true;
        }

        return false;
    }

    public void RemoveItem(ItemData item)
    {
        if (!Items.Contains(item)) return;
        Items.Remove(item);
        OnItemRemoved?.Invoke(item);
    }
}
