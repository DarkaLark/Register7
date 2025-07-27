using UnityEngine;

public class PlayerItemCarrier : MonoBehaviour
{
    [SerializeField] private Transform _carryAnchor;
    [SerializeField] private Vector3 _carryOffset = new Vector3(0f, 1f, .7f);

    private CarryableObject _currentItem;

    public bool HasItem => _currentItem != null;

    public void PickUpItem(CarryableObject item)
    {
        if (HasItem) return;

        _currentItem = item;
        item.SetCarrier(this);
        item.transform.SetParent(_carryAnchor);
        item.transform.localPosition = _carryOffset;
    }

    public void DropItem()
    {
        if (!HasItem) return;

        _currentItem.transform.SetParent(null);
        _currentItem.SetCarrier(null);
        _currentItem = null;
    }
}