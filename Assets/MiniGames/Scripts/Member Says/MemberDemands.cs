using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MemberDemands : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private List<ItemInformation> _allItems;
    private List<ItemInformation> _itemsWanted = new();

    private int _numberOfItems = 2;
    private int _maxItems = 5;

    private float _delayBetweenItems = 1f;
    private float _blankSpaceTime = 0.2f;

    private float _startDelayTimer = 1f;

    [SerializeField] private TurnStateGameEvent _onTurnStateChanged;
    [SerializeField] private ItemInformationGameEvent _sendItemInformation;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    void Start()
    {
        _numberOfItems = Mathf.Min(_numberOfItems + 1, _maxItems);

        _onGameStateChanged.Raise(GameState.MiniGame);

        StartCoroutine(DelayStart());
    }

    public void GenerateDemands()
    {
        _itemsWanted.Clear();

        if (_allItems == null || _allItems.Count == 0)
        {
            Debug.LogError("No items available in _allItems! Cannot generate demands.");
            return;
        }

        for (int i = 0; i < _numberOfItems; i++)
        {
            var choice = _allItems[Random.Range(0, _allItems.Count)];
            _itemsWanted.Add(choice);
        }

        StartCoroutine(PresentItems());

        _numberOfItems++;

        _onTurnStateChanged.Raise(TurnState.Player);
        _sendItemInformation.Raise(_itemsWanted);
    }

    private IEnumerator PresentItems()
    {
        int currentItemNumber = 0;

        while (currentItemNumber < _itemsWanted.Count)
        {
            ItemInformation currentItem = _itemsWanted[currentItemNumber];

            _text.text = string.Empty;
            PlayAudio(currentItem);
            yield return new WaitForSeconds(_blankSpaceTime);

            _text.text = currentItem.itemID.ToString();
            _text.color = currentItem.color;

            yield return new WaitForSeconds(_delayBetweenItems);
            currentItemNumber++;
        }
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(_startDelayTimer);
        GenerateDemands();
    }

    private void PlayAudio(ItemInformation currentItem)
    {
        if (_audioSource != null && currentItem != null)
        {
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.PlayOneShot(currentItem.itemSound);
        }
    }
}