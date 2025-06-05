using UnityEngine;

public class TargetOutlineController : MonoBehaviour
{
    private GameObject _lastTarget;

    void OnEnable()
    {
        InteractableDetector.OnBestTargetChanged += HandleTargetChanged;
    }
    
    void OnDisable()
    {
        InteractableDetector.OnBestTargetChanged -= HandleTargetChanged;
    }

    private void HandleTargetChanged(GameObject target)
    {
        if (_lastTarget != null && _lastTarget.TryGetComponent(out OutlineTarget lastOutline))
        {
            lastOutline.ShowOutline(false);
        }

        if (target != null && target.TryGetComponent(out OutlineTarget newOutline))
        {
            newOutline.ShowOutline(true);
        }

        _lastTarget = target;
    }
}