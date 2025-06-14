using UnityEngine;
using UnityEngine.InputSystem;

public class MemberSaysInputHandler : MonoBehaviour
{
    [SerializeField] private GameEvent _onHotdogPress;
    [SerializeField] private GameEvent _onPizzaPress;
    [SerializeField] private GameEvent _onIceCreamPress;
    [SerializeField] private GameEvent _onDrinkPress;

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
        _onHotdogPress.Raise();
    }

    void OnPizza(InputAction.CallbackContext context)
    {
        _onPizzaPress.Raise();
    }

    void OnIceCream(InputAction.CallbackContext context)
    {
        _onIceCreamPress.Raise();
    }

    void OnDrink(InputAction.CallbackContext context)
    {
        _onDrinkPress.Raise();
    }
}