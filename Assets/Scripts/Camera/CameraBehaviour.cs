using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float angleY = 45f;  // fixed horizontal angle
    [SerializeField]
    float angleX = 20f;  // fixed vertical tilt

    void LateUpdate()
    {
        // Create a rotation from the set angles
        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0f);

        // Apply rotation to the offset vector
        Vector3 angledOffset = rotation * offset;

        // Position the camera
        this.transform.position = target.position + angledOffset;

        // Look at the target
        this.transform.LookAt(target);
    }
}
