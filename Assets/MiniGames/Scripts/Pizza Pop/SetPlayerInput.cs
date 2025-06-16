using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerInput : MonoBehaviour
{
    private PlayerInput player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerInput>();

        player.SwitchCurrentActionMap("PizzaPop");
    }
}
