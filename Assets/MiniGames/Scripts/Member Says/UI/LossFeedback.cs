using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class LossFeedback : MonoBehaviour
{
    [Header("Events")]
    
    [SerializeField] private GameEvent _onPlayerLoss;
    [SerializeField] private GameEvent _onGenerateDemands;
    [SerializeField] private ItemInformationGameEvent _sendItemInformation;
    [SerializeField] private GameEvent _onMiniGameOver;

    private string _itemsWanted;    

    [Header("UI")]
    [Space(5)]
    private AudioSource _audioSource;
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
        _memberText.text = "I wanted " + _itemsWanted + "!!";

        _audioSource.PlayOneShot(_wrongBeep);

        yield return new WaitForSeconds(1f);
        _onMiniGameOver.Raise();
    }
}