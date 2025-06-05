using UnityEngine;

public class ExitMiniGame : MonoBehaviour
{
    [SerializeField] GameObject _miniGame;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    public void Exit()
    {
        Destroy(_miniGame);
        _onGameStateChanged.Raise(GameState.Playing);
    }
}