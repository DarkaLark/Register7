using UnityEngine;

public class PlayerDialogueInteract : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;

    [SerializeField] private GameEvent _onEKeyPress;

    void OnEnable()
    {
        _onEKeyPress.Register(InteractWithBestTarget);
    }

    void OnDisable()
    {
        _onEKeyPress.Unregister(InteractWithBestTarget);
    }

    private void InteractWithBestTarget()
    {
        if (_detector == null) return;
        _detector.Scan();

        GameObject bestTarget = _detector.BestTarget;
        if (bestTarget == null)
        {
            Debug.LogWarning("No valid target found to interact with.");
            return;
        }

        if (bestTarget.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
        else
        {
            Debug.LogWarning("No valid NPC found to interact with.");
        }
    }
}
