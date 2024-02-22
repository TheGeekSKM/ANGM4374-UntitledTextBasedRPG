using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    [TextArea(15, 20)]
    public string ItemDescription;
    public Vector2 DamageRange;
    public int Durability;
    public SoundData InteractionSound;
    public SoundData DiscardedSound;

    public virtual void UseItem()
    {

    }

    public virtual void DiscardItem()
    {

    }

}
