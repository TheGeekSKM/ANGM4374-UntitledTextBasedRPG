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

        var playerMovement = GameController.Instance.playerMovement;
        if (playerMovement)
        {
            //get a point within a radius of the player
            var randomPoint = Random.insideUnitSphere * 2;
            randomPoint += playerMovement.transform.position;
            randomPoint.y = playerMovement.transform.position.y;

            //create a sound at that point
            SoundManager.Instance.Sound(randomPoint, _itemData.DiscardedSound);
        }
        Destroy(gameObject);
    }
}
