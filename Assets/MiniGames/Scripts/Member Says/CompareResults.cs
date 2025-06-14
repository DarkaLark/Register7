using UnityEngine;
using System.Collections.Generic;

public class CompareResults : MonoBehaviour
{
    [SerializeField] private ItemInformationGameEvent _sendItemInformation;

    [SerializeField] private GameEvent _onGenerateDemands;

    [SerializeField] private PossibleItemsGameEvent _onPlayerResponse;

    private int _itemNumber = 0;
    private List<PossibleItems> _expectedItems = new();

    void OnEnable()
    {
        _sendItemInformation.Register(ListExpectedItems);

        _onPlayerResponse.Register(ReadItem);
    }

    void OnDisable()
    {
        _sendItemInformation.Unregister(ListExpectedItems);

        _onPlayerResponse.Unregister(ReadItem);
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