using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public RoomData CurrentRoom;
    public RoomData PreviousRoom;

    public void SetCurrentRoom(RoomData room)
    {
        if (CurrentRoom != null) PreviousRoom = CurrentRoom;
        CurrentRoom = room;
        RoomEntered();
    }

    public void FeelAroundRoom()
    {
        if (CurrentRoom == null) return;
        // if (CurrentRoom.NPC.InteractionCount == 0) RoomNPC();
        // showcase items in room
        if (CurrentRoom.Loot.Count > 0)
        {
            GameController.Instance.AddNotification("\nI think I found something in the room.");
            GameController.Instance.AddNotification("I should take a closer look.");
            RoomInventoryManager.Instance.OpenRoomInventory(CurrentRoom);
        }
        else
        {
            GameController.Instance.AddNotification("I don't think there's anything else in this room.");
        }
        
    }

    void RoomEntered()
    {
        if (CurrentRoom.discovered == false)
        {
            CurrentRoom.discovered = true;
            GameController.Instance.AddNotification("I think I found a new room!");
            GameController.Instance.AddNotification($"{CurrentRoom.StartingRoomDescription}");
        }
        else
        {
            GameController.Instance.AddNotification("I've been in this room before.");
            GameController.Instance.AddNotification($"{CurrentRoom.RoomDescription}");

            RoomNPC();

        }


    }

    public void RoomNPC()
    {
        if (CurrentRoom.NPC != null && CurrentRoom.NPC.InteractionCount == 0)
        {
            GameController.Instance.AddNotification($"I think I just ran in to someone in the {CurrentRoom.RoomName}.");
            DialogueManager.Instance.StartDialogue(CurrentRoom.NPC.IntroDialogueMoment);
            CurrentRoom.NPC.InteractionCount++;
        }
        else if (CurrentRoom.NPC != null && CurrentRoom.NPC.InteractionCount > 0)
        {
            GameController.Instance.AddNotification($"Oh damn it! I ran in to someone by accident!");
            GameController.Instance.AddNotification($"{CurrentRoom.NPC.name}: {CurrentRoom.NPC.InteractionLines[Random.Range(0, CurrentRoom.NPC.InteractionLines.Count)].DialogueLine}");
        }
    }

    public void RoomExited()
    {
        GameController.Instance.AddNotification($"I think I just left the {CurrentRoom.RoomName}.");
    }
}


