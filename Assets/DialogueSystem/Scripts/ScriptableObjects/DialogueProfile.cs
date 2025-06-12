using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/Dialogue Profile")]
public class DialogueProfile : ScriptableObject
{
    public string npcID;
    public List<DialogueBranch> dialogueBranches;
}