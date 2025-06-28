using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private Camera _currentMiniGameCamera;

    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SwitchToMainCamera()
    {
        if (_currentMiniGameCamera != null)
        {
            _currentMiniGameCamera.enabled = false;
            _currentMiniGameCamera = null;
        }

        _mainCamera.enabled = true;
    }

    public void SwitchToMiniGameCamera(Camera miniGameCamera)
    {
        _mainCamera.enabled = false;
        _currentMiniGameCamera = miniGameCamera;
        _currentMiniGameCamera.enabled = true;
    }

    public void DisableMainCamera() => _mainCamera.enabled = false;
    public void EnableMainCamera() => _mainCamera.enabled = true;
}