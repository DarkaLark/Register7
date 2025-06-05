
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    void LateUpdate()
    {
        transform.position = target.position + offset; 
    }
}
