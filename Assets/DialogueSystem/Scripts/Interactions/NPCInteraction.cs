using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueProfile _profile;

    public void Interact()
    {
        DialogueBranchSelector.TryPlayBestBranch(_profile);
    }
}
