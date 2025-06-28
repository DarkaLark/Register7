using UnityEngine;
using System.Collections;
using TMPro;

public class WonFeedback : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent _onPlayerWin;
    [SerializeField] private GameEvent _onGenerateDemands;
    [SerializeField] private GameEvent _onMiniGameOver;

    private AudioSource _audioSource;

    [Header("How Many Rounds")]
    [Space(5)]
    [SerializeField] private int _numberOfRounds = 2;
    private int _currentRound = 1;

    [Header("UI")]
    [Space(5)]
    [SerializeField] private TextMeshProUGUI _memberText;
    [SerializeField] private AudioClip _correctBeep;

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
            _onMiniGameOver.Raise();
        }
        else
        {
            _currentRound++;
            _onGenerateDemands.Raise();
        }
        
    }
}