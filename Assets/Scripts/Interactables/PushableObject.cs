using UnityEngine;

public class PushableObject  : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _pushPoint = new ();
    [SerializeField] private float _pushMass = 1f;
    private float _originalMass;
    
    private Rigidbody _rb;
    private PlayerObjectPusher _pusher;

    private void Awake()
    {
        if (TryGetComponent(out _rb))
            _originalMass = _rb.mass;
    }
    
    public void Interact()
    {
        Debug.Log("PushableObject");
        
        if (_pusher != null)
        {
            _pusher.StopPushing();
            return;
        }

        _pusher = FindFirstObjectByType<PlayerObjectPusher>();
        if (_pusher != null)
        {
            _pusher.StartPushing(this, _pushPoint);
        }
    }
    
    public void SetPusher(PlayerObjectPusher pusher)
    {
        _pusher = pusher;
        if (_pusher != null)
        {
            if (_rb != null) _rb.mass = _pushMass;
        }
        else
        {
            if (_rb != null) _rb.mass = _originalMass;
        }
    }
}