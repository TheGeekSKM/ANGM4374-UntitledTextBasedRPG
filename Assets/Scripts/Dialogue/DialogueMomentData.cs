using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Moment", menuName = "Dialogue/Dialogue Moment")]
public class DialogueMomentData : ScriptableObject
{
    public string MomentName;

    public int MomentInteractionNum;

    public List<DialogueLineData> DialogueLines;

    
}
