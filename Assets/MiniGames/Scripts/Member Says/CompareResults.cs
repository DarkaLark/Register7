using UnityEngine;
using System.Collections.Generic;

public class CompareResults : MonoBehaviour
{
    [SerializeField] private ItemInformationGameEvent _sendItemInformation;

    [SerializeField] private GameEvent _onGenerateDemands;

    [SerializeField] private PossibleItemsGameEvent _onHotdogPress;
    [SerializeField] private PossibleItemsGameEvent _onPizzaPress;
    [SerializeField] private PossibleItemsGameEvent _onIceCreamPress;
    [SerializeField] private PossibleItemsGameEvent _onDrinkPress;

    private int _itemNumber = 0;
    private List<PossibleItems> _expectedItems = new();

    void OnEnable()
    {
        _sendItemInformation.Register(ListExpectedItems);

        _onHotdogPress.Register(ReadItem);
        _onPizzaPress.Register(ReadItem);
        _onIceCreamPress.Register(ReadItem);
        _onDrinkPress.Register(ReadItem);
    }

    void OnDisable()
    {
        _sendItemInformation.Unregister(ListExpectedItems);

        _onHotdogPress.Unregister(ReadItem);
        _onPizzaPress.Unregister(ReadItem);
        _onIceCreamPress.Unregister(ReadItem);
        _onDrinkPress.Unregister(ReadItem);
    }

    private void ListExpectedItems(List<ItemInformation> items)
    {
        foreach (ItemInformation item in items)
        {
            _expectedItems.Add(item.itemID);
        }
    }

    private void ReadItem(PossibleItems item)
    {
        if (TurnStateManager.Instance.CurrentState == TurnState.Member) return;

        if (item >= _expectedItems[_itemNumber])
        {
            Debug.Log("Correct");
            _itemNumber++;
            Debug.Log(_itemNumber + " out of " + _expectedItems.Count);

            if (_itemNumber >= _expectedItems.Count)
            {
                ResetGame();
                Debug.Log("Should Restart");
            }
        }
        else
        {
            Debug.Log("Nah");
            _itemNumber++;

            ResetGame();
        }
    }

    private void ResetGame()
    {
        _onGenerateDemands.Raise();
        _itemNumber = 0;
        _expectedItems.Clear();
    }
}