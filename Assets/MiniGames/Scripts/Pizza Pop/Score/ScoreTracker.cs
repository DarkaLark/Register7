using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreTracker : MonoBehaviour
{
    [Header("Game Events")]
    [Space(5)]
    [SerializeField] private GameEvent _onBubbleSpawn;
    [SerializeField] private GameEvent _onBubblePop;
    [SerializeField] private GameEvent _onPizzaClick;

    [SerializeField] private GameEvent _onGiveResults;

    [Header("Results")]
    [Space(5)]
    [SerializeField] private GameObject _resultsPanel;
    [SerializeField] private GameObject _exitMiniGameObject;
    [SerializeField] private TextMeshProUGUI _exitText;
    [SerializeField] private TextMeshProUGUI _bubblesText;
    [SerializeField] private TextMeshProUGUI _pizzaText;

    private int _bubblesSpawned = 0;
    private int _bubblesPopped = 0;
    private int _pizzaClicks = 0;


    void OnEnable()
    {
        _onBubbleSpawn.Register(OnBubbleSpawn);
        _onBubblePop.Register(OnBubblePop);
        _onPizzaClick.Register(OnPizzaClick);
        _onGiveResults.Register(OnGiveResults);
    }

    void OnDisable()
    {
        _onBubbleSpawn.Unregister(OnBubbleSpawn);
        _onBubblePop.Unregister(OnBubblePop);
        _onPizzaClick.Unregister(OnPizzaClick);
        _onGiveResults.Unregister(OnGiveResults);
    }

    private void OnBubbleSpawn()
    {
        _bubblesSpawned++;
    }

    private void OnBubblePop()
    {
        _bubblesPopped++;
    }

    private void OnPizzaClick()
    {
        _pizzaClicks++;
    }

    private void OnGiveResults()
    {
        _resultsPanel.SetActive(true);
        _bubblesText.text = $"Bubbles Popped: {_bubblesPopped} / {_bubblesSpawned}";
        _pizzaText.text = $"Pizza Pokes: {_pizzaClicks}";

        StartCoroutine(ExitDelay());
    }

    private System.Collections.IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(2f);
        _exitMiniGameObject.SetActive(true);
        _exitText.DOColor(Color.white, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}