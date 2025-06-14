using UnityEngine;
using UnityEngine.InputSystem;

public class MemberSaysInputHandler : MonoBehaviour
{
    [SerializeField] private PossibleItemsGameEvent _onHotdogPress;
    [SerializeField] private PossibleItemsGameEvent _onPizzaPress;
    [SerializeField] private PossibleItemsGameEvent _onIceCreamPress;
    [SerializeField] private PossibleItemsGameEvent _onDrinkPress;

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
        _onHotdogPress.Raise(PossibleItems.Hotdog);
    }

    void OnPizza(InputAction.CallbackContext context)
    {
        _onPizzaPress.Raise(PossibleItems.Pizza);
    }

    void OnIceCream(InputAction.CallbackContext context)
    {
        _onIceCreamPress.Raise(PossibleItems.IceCream);
    }

    void OnDrink(InputAction.CallbackContext context)
    {
        _onDrinkPress.Raise(PossibleItems.Drink);
    }
}