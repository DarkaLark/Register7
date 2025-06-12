using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineTarget : MonoBehaviour
{
    private bool _isOutlined = false;
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void ShowOutline(bool show)
    {
        if (_isOutlined == show) return;
        _isOutlined = show;

        outline.enabled = show;
    }
}