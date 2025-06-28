using UnityEngine;

public class StartMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _miniGamePrefab;

    public void Interact()
    {
        Instantiate(_miniGamePrefab);
    }
}