using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _countdownNumbers;
    [SerializeField] private float _durationPerNumber = 1f;

    private AudioSource _audio;
    [SerializeField] private AudioClip _countdownBeep, _finishedBeep;

    private int _scaleSizeMultiplier = 9;

    [Header("Events")]
    [SerializeField] private GameEvent _onCountdownFinished;

    void Awake()
    {
        _audio = FindFirstObjectByType<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(StartBuffer());
    }

    private IEnumerator StartBuffer()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        PlayCountdown().ConfigureAwait(false);
    }

    private async Task PlayCountdown()
    {
        foreach (GameObject number in _countdownNumbers)
        {
            Vector3 objSize = Vector3.one * _scaleSizeMultiplier;

            number.SetActive(true);
            number.transform.localScale = Vector3.zero;
            int numberIndex = _countdownNumbers.IndexOf(number);

            bool isGo = DetermineWhichBeep(numberIndex);
            PlayAudio(isGo);

            await number.transform.
                DOScale(objSize, _durationPerNumber).
                SetEase(Ease.OutBack).
                SetUpdate(true).
                AsyncWaitForCompletion();

            number.SetActive(false);
        }

        _onCountdownFinished.Raise();
        Time.timeScale = 1;
        Destroy(gameObject);
    }

#region PlayerCountdown Funcs
        private void PlayAudio(bool isGo)
        {
            if (_audio != null && isGo) 
                _audio.PlayOneShot(_finishedBeep);
            else
                _audio.PlayOneShot(_countdownBeep);
        }
    
        private bool DetermineWhichBeep(int numberIndex)
        {
            return numberIndex == _countdownNumbers.Count - 1;
        }
#endregion
}
