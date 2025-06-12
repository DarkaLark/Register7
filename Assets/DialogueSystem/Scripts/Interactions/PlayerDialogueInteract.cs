using UnityEngine;

public class PlayerDialogueInteract : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;

    [SerializeField] private GameEvent _onInteractPress;

    void OnEnable()
    {
        _onInteractPress.Register(InteractWithBestTarget);
    }

    void OnDisable()
    {
        _onInteractPress.Unregister(InteractWithBestTarget);
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

        if (bestTarget.TryGetComponent(out IInteractable interactable)
        && interactable is MonoBehaviour mb && mb.enabled)
        {
            interactable.Interact();
        }
        else
        {
            Debug.LogWarning("No valid NPC found to interact with.");
        }
    }
}
