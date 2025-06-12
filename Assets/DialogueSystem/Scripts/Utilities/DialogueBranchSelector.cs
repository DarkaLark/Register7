using UnityEngine;

public static class DialogueBranchSelector
{
    public static bool TryPlayBestBranch(DialogueProfile profile)
    {
        foreach (var branch in profile.dialogueBranches)
        {
            var speaker = string.IsNullOrEmpty(branch.Dialogue.speakerName)
                ? profile.npcID
                : branch.Dialogue.speakerName;
            var requiredFlag = branch.DialogueFlags.RequiredFlag;
            var setFlagAfter = branch.DialogueFlags.SetFlagAfter;

            if (string.IsNullOrEmpty(requiredFlag) || GameFlags.HasFlag(requiredFlag))
            {
                DialogueManager.Instance.StartDialogue(branch.Dialogue, speaker);

                if (!string.IsNullOrEmpty(setFlagAfter))
                {
                    GameFlags.SetFlag(setFlagAfter);
                }

                return true;
            }
        }

        Debug.LogWarning($"No valid dialogue found for {profile.npcID}.");
        return false;
    }
}