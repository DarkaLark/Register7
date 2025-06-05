using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour2 : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float rotationSpeed = 5f;

    float currentYaw = 0f;  // horizontal rotation

    void LateUpdate()
    {
        // Get horizontal input (mouse X or right analog stick X)
        float horizontalInput = Input.GetAxis("Mouse X"); // for controller: use Input.GetAxis("RightStickHorizontal")

        // Update rotation angle
        currentYaw += horizontalInput * rotationSpeed;

        // Calculate new position with rotation
        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);
        Vector3 rotatedOffset = rotation * offset;

        // Update camera position
        this.transform.position = target.position + rotatedOffset;

        // Always look at the target
        this.transform.LookAt(target);
    }
}
