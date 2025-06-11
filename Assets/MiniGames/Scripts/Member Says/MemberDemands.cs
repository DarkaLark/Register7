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

    private int _numberOfItems = 3;
    private int _maxItems = 6;

    private float _delayBetweenItems = 1f;
    private float _blankSpaceTime = 0.2f;

    void Start()
    {
        _numberOfItems = Mathf.Min(_numberOfItems + 1, _maxItems);
    }

    public void GenerateDemands()
    {
        _itemsWanted.Clear();


        for (int i = 0; i < _numberOfItems; i++)
        {
            var choice = _allItems[Random.Range(0, _allItems.Count)];
            _itemsWanted.Add(choice);
        }

        StartCoroutine(PresentItems());

        _numberOfItems++;
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

            yield return new WaitForSeconds(_delayBetweenItems);
            currentItemNumber++;
        }
    }

    private void PlayAudio(ItemInformation currentItem)
    {
        Debug.Log("Attempting to play audio");
        if (_audioSource != null && currentItem != null)
        {
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.PlayOneShot(currentItem.itemSound);
            Debug.Log("Audio played");
        }
    }
}