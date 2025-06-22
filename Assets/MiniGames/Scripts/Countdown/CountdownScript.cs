using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _countdownNumbers;
    [SerializeField] private float _durationPerNumber = 1f;

    private int _scaleSizeMultiplier = 9;

    public async Task PlayCountdown()
    {
        foreach (GameObject number in _countdownNumbers)
        {
            Vector3 objSize = Vector3.one * _scaleSizeMultiplier;

            number.SetActive(true);
            number.transform.localScale = Vector3.zero;

            await number.transform.
                DOScale(objSize, _durationPerNumber).
                SetEase(Ease.OutBack).
                AsyncWaitForCompletion();

            number.SetActive(false);
        }
    }
}
