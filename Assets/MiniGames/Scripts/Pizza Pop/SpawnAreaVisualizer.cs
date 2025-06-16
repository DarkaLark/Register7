using UnityEngine;

public class SpawnAreaVisualizer : MonoBehaviour
{
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private int _segments = 16;
    [SerializeField] private Color _gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;

        Vector3 center = transform.position;
        float angleStep = 360f / _segments;
        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= _segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            Vector3 point = new Vector3(x, 0f, z) + center;

            if (i > 0)
                Gizmos.DrawLine(prevPoint, point);

            prevPoint = point;
        }
    }
}
