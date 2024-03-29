using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    public string ItemName;
    public List<string> UseageDescriptions;
    public Vector2 DamageRange;
    public int Durability = -1;
    
    [Header("Sounds")]
    public SoundData PickupSound;
    public SoundData UseageSound;
    public SoundData DiscardedSound;


    public virtual void PickupItem()
    {

    }

    public virtual void UseItem()
    {
        if (UseageDescriptions.Count > 0)
        {
            GameController.Instance.AddNotification(UseageDescriptions[Random.Range(0, UseageDescriptions.Count)]);
        }

        if (Durability > 0)
        {
            Durability--;
        }
    }

    public virtual void DiscardItem()
    {

    }

}
