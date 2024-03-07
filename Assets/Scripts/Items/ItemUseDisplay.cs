using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseDisplay : ItemDisplay
{
    public override void OnButtonClick()
    {
        _itemData.UseItem();
        if (_itemData.Durability == 0)
        {
            Destroy(gameObject);
        }
    }
}
