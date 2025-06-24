using UnityEngine;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    [Header("Despawn Timer")]
    [Space(5)]
    [SerializeField] private float _despawnTimer = 3f;

    [Header("Tween")]
    [Space(5)]
    private Tween _scaleTween;
    [SerializeField] private float _miniTweenSize = 0.1f;
    [SerializeField] private float _maxTweenSize = 0.2f;

    [Header("Audio")]
    [Space(5)]
    [SerializeField] AudioClip _popAudio;
    private AudioSource _audio;

    [Header("Game Events")]
    [Space(5)]
    [SerializeField] private GameEvent _onBubblePop;
    [SerializeField] private GameEvent _onGiveResults;

    public bool IsLastBubble { get; set; }

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
            if (IsLastBubble)
                _onGiveResults.Raise();
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

        _onBubblePop.Raise();

        if (IsLastBubble)
            _onGiveResults.Raise();

        Destroy(gameObject);
    }
}