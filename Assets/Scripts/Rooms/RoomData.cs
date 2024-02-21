using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class RoomData : ScriptableObject
{
    public string RoomName;

    [TextArea(15, 20)]
    public string StartingRoomDescription;

    [TextArea(15, 20)]
    public string RoomDescription;


    //needs list of items on walls

    //needs list of items on floor

    public NPCData NPC;

    public List<ItemData> Loot;

    public bool discovered = false;

    
}
