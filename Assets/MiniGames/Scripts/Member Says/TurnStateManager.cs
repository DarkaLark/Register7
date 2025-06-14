using UnityEngine;

public class TurnStateManager : MonoBehaviour
{
    public static TurnStateManager Instance { get; private set; }
    private TurnState _currentState;
    public TurnState CurrentState => _currentState;

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
}