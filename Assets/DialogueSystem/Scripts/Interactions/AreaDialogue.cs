using UnityEngine;

public class AreaDialogue : MonoBehaviour
{
    [SerializeField] private DialogueProfile _profile;
    [SerializeField] private bool _onlyOnce = true;

    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || (_hasTriggered && _onlyOnce)) return;

        PlayBestDialogueBranch();
        _hasTriggered = true;
    }

    private void PlayBestDialogueBranch()
    {
        DialogueBranchSelector.TryPlayBestBranch(_profile);
    }
}
