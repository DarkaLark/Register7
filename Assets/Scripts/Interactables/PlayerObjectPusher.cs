using UnityEngine;

public class PlayerObjectPusher : MonoBehaviour
{
    [SerializeField] private Transform _pusherAnchor;
    
    private PushableObject _currentItem;
    
    public bool HasItem => _currentItem != null;

    public void StartPushing(PushableObject item, Vector3 pushPoint)
    {
        if (HasItem) return;
        Debug.Log("StartPushing");
        
        gameObject.transform.localPosition = pushPoint;

        item.SetPusher(this);
        item.transform.SetParent(_pusherAnchor);
        _currentItem = item;
    }

    public void StopPushing()
    {
        if (!HasItem) return;
        Debug.Log("StopPushing");
        
        _currentItem.SetPusher(this);
        _currentItem = null;
    }
}