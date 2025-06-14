using UnityEngine;
using UnityEngine.InputSystem;

public class MemberSaysInputHandler : MonoBehaviour
{
    [SerializeField] private PossibleItemsGameEvent _onPlayerResponse;

    private PlayerInput _playerInput;

    private InputAction _hotdog;
    private InputAction _pizza;
    private InputAction _iceCream;
    private InputAction _drink;

    void Awake()
    {
        _playerInput = FindFirstObjectByType<PlayerInput>();

        _hotdog = _playerInput.actions["Hotdog"];
        _pizza = _playerInput.actions["Pizza"];
        _iceCream = _playerInput.actions["Ice Cream"];
        _drink = _playerInput.actions["Drink"];
    }
    void OnEnable()
    {
        _hotdog.performed += OnHotdog;
        _pizza.performed += OnPizza;
        _iceCream.performed += OnIceCream;
        _drink.performed += OnDrink;
    }

    void OnDisable()
    {
        _hotdog.performed -= OnHotdog;
        _pizza.performed -= OnPizza;
        _iceCream.performed -= OnIceCream;
        _drink.performed -= OnDrink;
    }

    void OnHotdog(InputAction.CallbackContext context)
    {
        OnPlayerResponse(PossibleItems.Hotdog);
    }

    void OnPizza(InputAction.CallbackContext context)
    {
        OnPlayerResponse(PossibleItems.Pizza);
    }

    void OnIceCream(InputAction.CallbackContext context)
    {
        OnPlayerResponse(PossibleItems.IceCream);
    }

    void OnDrink(InputAction.CallbackContext context)
    {
        OnPlayerResponse(PossibleItems.Drink);
    }

    private void OnPlayerResponse(PossibleItems item)
    {
        _onPlayerResponse.Raise(item);
    }
}