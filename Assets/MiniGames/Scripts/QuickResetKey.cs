using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuickResetKey : MonoBehaviour
{
    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _playerInput.actions["Restart"].performed += ctx => OnRestart();
    }

    void OnDisable()
    {
        _playerInput.actions["Restart"].performed -= ctx => OnRestart();
    }


    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}