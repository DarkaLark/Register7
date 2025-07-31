using UnityEngine;

public class CarryableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _itemOffset = new (0f, 1f, .7f);

    
    private Rigidbody _rb;
    private Collider _collider;
    private PlayerItemCarrier _carrier;
    
    void Awake()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _collider);
    }

    public void Interact()
    {
        if (_carrier != null)
        {
            _carrier.DropItem();
            return;
        }

        _carrier = FindFirstObjectByType<PlayerItemCarrier>();
        if (_carrier != null)
        {
            _carrier.PickUpItem(this, _itemOffset);
        }
    }

    public void SetCarrier(PlayerItemCarrier carrier)
    {
        _carrier = carrier;
        if (carrier != null)
        {
            if (_rb != null) _rb.isKinematic = true;
            if (_collider != null) _collider.enabled = false;
        }
        else
        {
            if (_rb != null) _rb.isKinematic = false;
            if (_collider != null) _collider.enabled = true;
        }
    }
}