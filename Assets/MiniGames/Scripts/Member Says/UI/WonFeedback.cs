using UnityEngine;
using TMPro;
using System.Collections;

public class WonFeedback : MonoBehaviour
{
    [SerializeField] private GameEvent _onPlayerWin;

    [SerializeField] private GameEvent _onGenerateDemands;

    [SerializeField] TextMeshProUGUI _memberText;
    [SerializeField] AudioClip _correctBeep;

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
        AudioSource.PlayClipAtPoint(_correctBeep, Camera.main.transform.position);
        yield return new WaitForSeconds(2);

        _onGenerateDemands.Raise();
    }
}