using UnityEngine;

public class TurnStateManager : MonoBehaviour
{
    public static TurnStateManager Instance { get; private set; }
    private TurnState _currentState;
    public TurnState CurrentState => _currentState;

    [SerializeField] private TurnStateGameEvent _onTurnStateChanged;

    void OnEnable()
    {
        _onTurnStateChanged.Register(SetState);
    }

    void OnDisable()
    {
        _onTurnStateChanged.Unregister(SetState);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetState(TurnState newState)
    {
        _currentState = newState;
    }
}