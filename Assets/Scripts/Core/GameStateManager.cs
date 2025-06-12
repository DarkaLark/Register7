using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    MiniGame
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; } = GameState.Playing;

    [SerializeField] private GameStateGameEvent _onGameStateChanged;

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