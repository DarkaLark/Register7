using UnityEngine;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float _despawnTimer = 3f;

    private Tween _scaleTween;
    [SerializeField] private float _miniTweenSize = 0.1f;
    [SerializeField] private float _maxTweenSize = 0.2f;

    [SerializeField] AudioClip _popAudio;
    private AudioSource _audio;

    void Awake()
    {
        _audio = FindFirstObjectByType<AudioSource>();   
    }

    void Start()
    {
        _scaleTween = AnimateBubbleGrowth();
    }

#region AnimateBubbleGrowth func's
        private Tween AnimateBubbleGrowth()
        {
            Vector3 sizeTween = GetRandomScale();
    
            return transform.DOScale(sizeTween, _despawnTimer)
                .SetEase(Ease.OutBack);
        }
    
        private Vector3 GetRandomScale()
        {
            return new(
                Random.Range(_miniTweenSize, _maxTweenSize),
                Random.Range(_miniTweenSize, _maxTweenSize),
                Random.Range(_miniTweenSize, _maxTweenSize)
            );
        }

    void OnDestroy()
    {
        _scaleTween?.Kill();
    }
#endregion

    void Update()
    {
        _despawnTimer -= Time.deltaTime;
        if (_despawnTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        if (_audio != null)
        {
            _audio.pitch = Random.Range(.95f, 1.05f);
            _audio.PlayOneShot(_popAudio);
        }

        Destroy(gameObject);
    }
}