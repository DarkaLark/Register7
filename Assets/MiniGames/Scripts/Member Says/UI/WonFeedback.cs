using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class WonFeedback : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlayerWin;

    [SerializeField] private GameEvent _onGenerateDemands;
    [SerializeField] GameObject _backButton;

    private AudioSource _audioSource;

    [Space(5)]
    [Header("How Many Rounds")]
    [SerializeField] private int _numberOfRounds = 2;
    private int _currentRound = 1;

    [Space(5)]
    [SerializeField] TextMeshProUGUI _memberText;
    [SerializeField] AudioClip _correctBeep;

    void Awake()
    {
        _audioSource = FindFirstObjectByType<AudioSource>();   
    }

    void OnEnable()
    {
        _onPlayerWin.Register(Feedback);
    }

    void OnDisable()
    {
        _onPlayerWin.Unregister(Feedback);
    }

    private void Feedback()
    {
        StartCoroutine(FeedbackCoroutine());
    }

    private IEnumerator FeedbackCoroutine()
    {
        _memberText.color = Color.black;
        _memberText.text = "Thanks!!";

        _audioSource.PlayOneShot(_correctBeep);
        yield return new WaitForSeconds(2);

        if (_currentRound >= _numberOfRounds)
        {
            _backButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_backButton);
        }
        else
        {
            _currentRound++;
            _onGenerateDemands.Raise();
        }
        
    }
}