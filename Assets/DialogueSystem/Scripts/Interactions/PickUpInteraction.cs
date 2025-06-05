using UnityEngine;

public class PickUpInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private FlagCondition gameFlags;
    public void Interact()
    {
        string requiredFlag = gameFlags.RequiredFlag;
        string setFlag = gameFlags.SetFlagAfter;

        if (string.IsNullOrEmpty(requiredFlag) || GameFlags.HasFlag(requiredFlag))
        {
            Destroy(gameObject);
            if (!string.IsNullOrEmpty(setFlag))
            {
                GameFlags.SetFlag(setFlag);
            }
        }
    }
}
