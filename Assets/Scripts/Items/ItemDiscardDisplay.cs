using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiscardDisplay : ItemDisplay
{
    public override void OnButtonClick()
    {
        _itemData.DiscardItem();
        var currentRoom = InventoryManager.Instance.PlayerRoom.CurrentRoom;
        if (currentRoom) currentRoom.Loot.Add(_itemData);
        Destroy(gameObject);
    }
}
