using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineTarget : MonoBehaviour
{
    private bool _isOutlined = false;

    public void ShowOutline(bool show)
    {
        if (_isOutlined == show) return;
        _isOutlined = show;
    }
}