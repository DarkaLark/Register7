using UnityEngine;

public class PlayerItemCarrier : MonoBehaviour
{
    [SerializeField] private Transform _carryAnchor;

    private CarryableObject _currentItem;

    public bool HasItem => _currentItem != null;

    public void PickUpItem(CarryableObject item, Vector3 offset)
    {
        if (HasItem) return;

        _currentItem = item;
        item.SetCarrier(this);
        item.transform.SetParent(_carryAnchor);
        item.transform.localPosition = offset;
    }

    public void DropItem()
    {
        if (!HasItem) return;

        _currentItem.transform.SetParent(null);
        _currentItem.SetCarrier(null);
        _currentItem = null;
    }
}