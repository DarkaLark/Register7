using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _miniGameCamera;

    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void SwitchToMainCamera()
    {
        _mainCamera.enabled = true;
        _miniGameCamera.enabled = false;
    }

    public void SwitchToMiniGameCamera()
    {
        _mainCamera.enabled = false;
        _miniGameCamera.enabled = true;
    }
}