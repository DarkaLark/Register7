using UnityEngine;

public enum DialogueState
{
    None,
    Listening,
    Responding
}

public class DialogueStateManager : MonoBehaviour
{
    public static DialogueStateManager Instance { get;  private set; }

    public static DialogueState CurrentState { get; private set; } = DialogueState.None;

    [SerializeField] private DialogueStateGameEvent _onDialogueStateChanged;

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

    public void SetState(DialogueState newState)
    {
        CurrentState = newState;
        _onDialogueStateChanged.Raise(newState);
    }
}
