using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC Data")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    public string HiddenName;
    
    [TextArea(15, 20)]
    public string NPCDescription;

    public int InteractionCount = 0;
    public DialogueMomentData IntroDialogueMoment;
    public List<DialogueLineData> InteractionLines;

}
