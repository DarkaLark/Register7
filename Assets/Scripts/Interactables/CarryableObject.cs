using UnityEngine;

/*
 * Attach to object that want top be carrier
 * and dropped
 */

public class CarryableObject : MonoBehaviour, IInteractable
{ 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private Vector3 _itemOffset = new Vector3(0f, 1.2f, .7f);
    private PlayerItemCarrier _carrier;

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
        if (_carrier != null)
        {
            if ( _rigidbody != null) _rigidbody.isKinematic = true;
            if (_collider != null) _collider.enabled = false;
        }
        else
        {
            if  ( _rigidbody != null) _rigidbody.isKinematic = false;
            if (_collider != null) _collider.enabled = true;
        }
    }
}