using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public RoomData roomData;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered room");
            RoomEntered();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited room");
            RoomExited();
        }
    }

    void RoomEntered()
    {
        if (roomData.discovered == false)
        {
            roomData.discovered = true;
            GameController.Instance.AddNotification("I think I found a new room!");
            GameController.Instance.AddNotification($"{roomData.StartingRoomDescription}");
        }
        else
        {
            GameController.Instance.AddNotification("I've been in this room before.");
            GameController.Instance.AddNotification($"{roomData.RoomDescription}");

        }
    }

    void RoomExited()
    {
        GameController.Instance.AddNotification($"I think I just left the {roomData.RoomName}.");
    }
}
