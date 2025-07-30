using UnityEngine;

public class PushableObject  : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("PushableObject");
    }
}