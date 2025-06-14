using UnityEngine;

public class RegisterResponse : MonoBehaviour
{
    [SerializeField] private GameEvent _onHotdogPress;
    [SerializeField] private GameEvent _onPizzaPress;
    [SerializeField] private GameEvent _onIceCreamPress;
    [SerializeField] private GameEvent _onDrinkPress;

    void OnEnable()
    {
        _onHotdogPress.Register(HotdogResponse);
        _onPizzaPress.Register(PizzaResponse);
        _onIceCreamPress.Register(IceCreamResponse);
        _onDrinkPress.Register(DrinkResponse);
    }

    void OnDisable()
    {
        _onHotdogPress.Unregister(HotdogResponse);
        _onPizzaPress.Unregister(PizzaResponse);
        _onIceCreamPress.Unregister(IceCreamResponse);
        _onDrinkPress.Unregister(DrinkResponse);
    }

    private void HotdogResponse()
    {
        Debug.Log("Hotdog Pressed");
    }
    
    private void PizzaResponse()
    {
        Debug.Log("Pizza Pressed");
    }
    private void IceCreamResponse()
    {
        Debug.Log("Ice Cream Pressed");
    }
    private void DrinkResponse()
    {
        Debug.Log("Drink Pressed");
    }

}
