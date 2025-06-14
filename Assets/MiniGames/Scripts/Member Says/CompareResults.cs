using UnityEngine;
using System.Collections.Generic;

public class CompareResults : MonoBehaviour
{
    [SerializeField] private ItemInformationGameEvent _sendItemInformation;

    [Space(5)]
    [SerializeField] private GameEvent _onPlayerWin;
    [SerializeField] private GameEvent _onPlayerLoss;

    [Space(5)]
    [SerializeField] private PossibleItemsGameEvent _onPlayerResponse;

    private int _currentItemIndex = 0;
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
        _expectedItems.Clear();
        foreach (ItemInformation item in items)
        {
            _expectedItems.Add(item.itemID);
        }

        Debug.Log("Expected sequence: " + string.Join(", ", _expectedItems));
    }

    private void ReadItem(PossibleItems item)
    {
        Debug.Log(item + " being pressed");
        if (TurnStateManager.Instance.CurrentState == TurnState.Member) return;

        if (item == _expectedItems[_currentItemIndex])
        {
            Debug.Log("Correct");
            _currentItemIndex++;
            Debug.Log(_currentItemIndex + " out of " + _expectedItems.Count);

            if (_currentItemIndex >= _expectedItems.Count)
            {
                _onPlayerWin.Raise();
                ResetSequence();
            }
        }
        else
        {
            _onPlayerLoss.Raise();
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        _currentItemIndex = 0;
        _expectedItems.Clear();
    }
}