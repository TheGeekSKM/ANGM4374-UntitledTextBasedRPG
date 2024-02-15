using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/Dialogue Line")]
public class DialogueLineData : ScriptableObject
{
    public NPCData NPCData;

    [TextArea(15, 20)]
    public string DialogueLine;
    public float TypeDelay = 0.05f;
}
