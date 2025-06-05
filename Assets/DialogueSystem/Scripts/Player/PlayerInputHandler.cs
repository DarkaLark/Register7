using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onEKeyPress;

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _playerInput.actions["Interact"].performed += ctx => OnInteract();
        _playerInput.actions["Restart"].performed += ctx => OnRestart();

    }

    void OnDisable()
    {
        _playerInput.actions["Interact"].performed -= ctx => OnInteract();
        _playerInput.actions["Restart"].performed -= ctx => OnRestart();
    }

    private void OnInteract()
    {
        _onEKeyPress.Raise();
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameFlags.ResetGameFlags();
    }
}