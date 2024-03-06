using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public RoomData roomData;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    void OnTriggerEnter(Collider other)
    {
        var roomManager = other.gameObject.GetComponent<RoomManager>();
        if (roomManager == null) return;

        roomManager.SetCurrentRoom(roomData);
    }

    void OnTriggerExit(Collider other)
    {
        var roomManager = other.gameObject.GetComponent<RoomManager>();
        if (roomManager == null) return;
        roomManager.RoomExited();
    }

   
}
