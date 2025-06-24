using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private GameEvent _onBubbleSpawn;
    [SerializeField] private GameEvent _onBubblePop;
    [SerializeField] private GameEvent _onPizzaClick;

    [SerializeField] private GameEvent _onGiveResults;

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
        Debug.Log($"Bubbles Spawned: {_bubblesSpawned}");
        Debug.Log($"Bubbles Popped: {_bubblesPopped}");
        Debug.Log($"Pizza Clicks: {_pizzaClicks}");
    } 
}