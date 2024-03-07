using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SeekerWhistle", menuName = "Items/SeekerWhistle")]
public class SeekerWhistle : ItemData
{
    public override void UseItem()
    {
        QuestController.Instance.LocateQuestItem();
    }
}
