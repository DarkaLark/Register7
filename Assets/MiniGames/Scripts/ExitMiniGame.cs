using UnityEngine;

public class ExitMiniGame : MonoBehaviour
{
    [SerializeField] GameObject _miniGame;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    public void Exit()
    {
        _onGameStateChanged.Raise(GameState.Playing);
        Destroy(_miniGame);
    }
}