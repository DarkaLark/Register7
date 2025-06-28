using UnityEngine;

public class MiniGameRoot : MonoBehaviour
{
    [SerializeField] private Camera _miniGameCamera;
    [SerializeField] private GameStateGameEvent _onGameStateChanged;

    private void Start()
    {
        if (_miniGameCamera != null)
            CameraManager.Instance.SwitchToMiniGameCamera(_miniGameCamera);

        _onGameStateChanged.Raise(GameState.MiniGame);
    }

    public void ExitMiniGame()
    {
        if (_miniGameCamera != null)
            CameraManager.Instance.SwitchToMainCamera();

        _onGameStateChanged.Raise(GameState.Playing);
        
        Destroy(gameObject);
    }
}