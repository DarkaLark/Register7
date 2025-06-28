using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;
using System.Collections;

public class MiniGameFinished : MonoBehaviour
{
    [Header("Events")]
    [Space(5)]
    [SerializeField] private GameEvent _onMiniGameOver;
    [Header("UI")]
    [Space(5)]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _continueText;

    private PlayerInput _playerInput;
    private InputAction _exit;
    private MiniGameRoot _miniGameRoot;

    void Start()
    {
        _playerInput = FindFirstObjectByType<PlayerInput>();
        _miniGameRoot = FindFirstObjectByType<MiniGameRoot>();
    }
    void OnEnable()
    {
        _onMiniGameOver.Register(OnMiniGameOver);
    }

    void OnDisable()
    {
        _onMiniGameOver.Unregister(OnMiniGameOver);
        _exit.performed -= OnExit;
    }

    [ContextMenu("Show Continue Text")]
    private void OnMiniGameOver()
    {
        _panel.SetActive(true);
        _playerInput.SwitchCurrentActionMap("MiniGameFinished");

        _continueText.alpha = .2f;

        StartCoroutine(BlinkingText());
    }

    private IEnumerator BlinkingText()
    {
        yield return new WaitForSeconds(1f);
        
        _exit = _playerInput.actions["Exit"];
        _exit.performed += OnExit;

        _continueText.DOFade(1f, .5f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    private void OnExit(InputAction.CallbackContext ctx)
    {
        print("Exit Mini Game");
        _miniGameRoot.ExitMiniGame();
    }
}
