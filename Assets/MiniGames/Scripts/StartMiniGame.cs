using UnityEngine;

public class StartMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    public void Interact()
    {
        Instantiate(_miniGame);
        Debug.Log("Start Game: " + gameObject.name);
        _onGameStateChanged.Raise(GameState.MiniGame);
    }
}
