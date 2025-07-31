using UnityEngine;

public class PlayerObjectPusher : MonoBehaviour
{
    [SerializeField] private Transform _pusherAnchor;
    
    private PushableObject _currentItem;
    
    public bool HasItem => _currentItem != null;

    public void StartPushing(PushableObject item)
    {
        if (HasItem) return;
        Debug.Log("StartPushing");
        
        if (item != null)
        {
            Transform target = item.PushPoint;
            if (target != null)
            {
                transform.SetPositionAndRotation(target.position, target.rotation);
            }
        }

        item.SetPusher(this);
        if (_pusherAnchor != null)
        {
            item.transform.SetParent(_pusherAnchor, true);
        }
        _currentItem = item;
    }

    public void StopPushing()
    {
        if (!HasItem) return;
        Debug.Log("StopPushing");
        
        _currentItem.transform.SetParent(null, true);
        _currentItem.SetPusher(null);
        _currentItem = null;
    }
}