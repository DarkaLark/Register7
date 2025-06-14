using UnityEngine;

public class StartMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    public void Interact()
    {
        Instantiate(_miniGame);
        _onGameStateChanged.Raise(GameState.MiniGame);
        Debug.Log(_miniGame.name);
    }
}
