using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiscardDisplay : ItemDisplay
{
    public override void OnButtonClick()
    {
        _itemData.DiscardItem();
        Destroy(gameObject);
    }
}
