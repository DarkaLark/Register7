using UnityEngine;
using UnityEngine.EventSystems;

public class ExitMiniGame : MonoBehaviour
{
    [SerializeField] GameObject _miniGame;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Exit();
        }
    }

    private void Exit()
    {
        EventSystem.current.SetSelectedGameObject(_miniGame);
        _onGameStateChanged.Raise(GameState.Playing);
        Destroy(_miniGame);
    }
}