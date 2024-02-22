using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInventoryManager : MonoBehaviour
{
    public static RoomInventoryManager Instance {get; private set;}
    public GameObject RoomContentPanel;
    public GameObject ItemDisplayPrefab;

    public List<ItemData> RoomDisplayItems;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenCurrentRoomInventory()
    {
        var currentRoom = GameController.Instance.playerMovement.GetComponent<RoomManager>().CurrentRoom;
        if (currentRoom == null) return;
        if (currentRoom.Loot.Count == 0) return;

        OpenRoomInventory(currentRoom);
    }

    public void OpenRoomInventory(RoomData roomData)
    {
        // Clear the room content panel
        foreach (Transform child in RoomContentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        RoomDisplayItems = new List<ItemData>();

        // Add the items to the room content panel
        foreach (ItemData item in roomData.Loot)
        {
            GameObject itemDisplay = Instantiate(ItemDisplayPrefab, RoomContentPanel.transform);
            itemDisplay.GetComponent<ItemDisplay>().SetItem(item);
            RoomDisplayItems.Add(item);
        }

        // Open the room inventory panel
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameRoomInventoryState, .5f);
    }

    public void CloseRoomInventory()
    {
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GamePlayState, .5f);
    }
}
