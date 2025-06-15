using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class LossFeedback : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlayerLoss;

    [SerializeField] private ItemInformationGameEvent _sendItemInformation;
    private string _itemsWanted;

    [SerializeField] private GameObject _backButton;

    private AudioSource _audioSource;
    
    [SerializeField] private GameEvent _onGenerateDemands;

    [Space(5)]
    [SerializeField] TextMeshProUGUI _memberText;
    [SerializeField] AudioClip _wrongBeep;

    void Awake()
    {
        _audioSource = FindFirstObjectByType<AudioSource>();
    }

    void OnEnable()
    {
        _onPlayerLoss.Register(Feedback);
        _sendItemInformation.Register(ListExpectedItems);
    }

    void OnDisable()
    {
        _onPlayerLoss.Unregister(Feedback);
        _sendItemInformation.Unregister(ListExpectedItems);
    }

    private void ListExpectedItems(List<ItemInformation> items)
    {
    var names = items.ConvertAll(item => item.itemID.ToString());

    _itemsWanted = string.Join(", ", names.GetRange(0, names.Count - 1))
        + ", and " + names[names.Count - 1];
    }

    private void Feedback()
    {
        StartCoroutine(FeedbackCoroutine());
    }

    private IEnumerator FeedbackCoroutine()
    {
        TurnStateManager.Instance.SetState(TurnState.Member);

        _memberText.color = Color.black;
        _memberText.text = "I wanted " + _itemsWanted +"!!";

        _audioSource.PlayOneShot(_wrongBeep);
        yield return new WaitForSeconds(2);
        
            _backButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_backButton);
    }
}