using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueUIReferences _ui;

    [SerializeField] private DialogueAudioReferences _audio;

    [Header("Camera")]
    [Space(3)]
    [SerializeField] private GameObject _camera;

    [Header("Typing Dialogue")]
    [Space(5)]
    [SerializeField] private float _typingSpeed = 0.02f;
    [SerializeField] private float _baseVolume = 1f;
    [Tooltip("Lower volume when inscreasing dialogue speed.")]
    [SerializeField] private float _fastTypeVolumeMultiplier = 0.25f;
    [Tooltip("Prevents too many overlapping voices when typing fast. Higher: Less voicing/ Lower: More voicing.")]
    [SerializeField] private float _blipCooldown = 0.0025f;

    [Header("Dialogue State Handler")]
    [Space(5)]
    [SerializeField] private DialogueStateGameEvent _onDialogueChanged;
    [SerializeField] private GameEvent _onAdvanceDialogue;

    private float _blipTimer = 0f;
    private AudioClip _currentBlip;
    private const float HoldThreshold = 0.2f;
    private float _mouseHoldTime = 0f;


    private DialogueNode _currentNode;
    private int _currentLineIndex = 0;
    private Coroutine _typingCoroutine;
    private bool _isTyping = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        _onAdvanceDialogue.Register(TryAdvancingDialogue);
    }

    void OnDisable()
    {
        _onAdvanceDialogue.Unregister(TryAdvancingDialogue);
    }

    public void StartDialogue(DialogueNode startingNode, string overrideSpeaker = null)
    {
        _ui.DialoguePanel.SetActive(true);
        _ui.SpeakerNameText.text = string.IsNullOrEmpty(overrideSpeaker)
            ? startingNode.speakerName
            : overrideSpeaker;

        LoadNode(startingNode);
    }

    private void LoadNode(DialogueNode node)
    {
        ClearPreviousChoices();

        _onDialogueChanged.Raise(DialogueState.Listening);
        _currentNode = node;
        _currentLineIndex = 0;

        _camera.SetActive(false);

        _currentBlip = node.voiceBlip != null ? node.voiceBlip : _audio.DefaultBlip;

        StartTyping(_currentNode.lines[_currentLineIndex]);
    }

    private void TryAdvancingDialogue()
    {
        if (_isTyping || ChoiceToBeMade()) return;

        AdvanceToNextLine();
    }
    private void ShowChoices()
    {
        ClearPreviousChoices();
        List<DialogueNode.DialogueChoice> currentNodeChoices = _currentNode.choices;

        if (currentNodeChoices != null && currentNodeChoices.Count > 0 && !AreAllChoicesFlagged(currentNodeChoices))
        {
            _onDialogueChanged.Raise(DialogueState.Responding);

            _ui.ChoicePanel.SetActive(true);
            GameObject firstSelectable = null;

            foreach (var choice in currentNodeChoices)
            {
                string requiredFlag = choice.dialogueFlags.RequiredFlag;

                if (string.IsNullOrEmpty(requiredFlag) || GameFlags.HasFlag(requiredFlag))
                {
                    GameObject choiceButton = CreateChoiceButton(choice);

                    if (firstSelectable == null)
                        firstSelectable = choiceButton;
                }
            }

            if (firstSelectable != null)
                StartCoroutine(SetSelectionNextFrame(firstSelectable));
        }
        else
        {
            EndDialogue();
        }
    }

    private bool AreAllChoicesFlagged(List<DialogueNode.DialogueChoice> currentNodeChoices)
    {
        foreach (var choice in currentNodeChoices)
        {
            string requiredFlag = choice.dialogueFlags.RequiredFlag;

            if (string.IsNullOrEmpty(requiredFlag) || GameFlags.HasFlag(requiredFlag))
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator TypeLine(string line)
    {
        _isTyping = true;
        _ui.DialogueLineText.text = "";
        _blipTimer = 0f;

        foreach (char letter in line)
        {
            _ui.DialogueLineText.text += letter;
            _blipTimer -= Time.deltaTime;

            PlayBlip(letter);

            float delay = (letter == ' ') ? 0f : (_mouseHoldTime >= HoldThreshold ? _typingSpeed * 0.25f : _typingSpeed);

            yield return new WaitForSeconds(delay);
        }

        _mouseHoldTime = 0f;
        _isTyping = false;
    }

    private void ClearPreviousChoices()
    {
        foreach (Transform child in _ui.ChoicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void EndDialogue()
    {
        _ui.DialoguePanel.SetActive(false);
        _ui.ChoicePanel.SetActive(false);
        _currentNode = null;
        _currentLineIndex = 0;
        _camera.SetActive(true);
        _onDialogueChanged.Raise(DialogueState.None);
    }

    private bool ChoiceToBeMade()
    {
        return _ui.ChoicesContainer.childCount > 0;
    }

    private GameObject CreateChoiceButton(DialogueNode.DialogueChoice choice)
    {
        GameObject btnObj = Instantiate(_ui.ChoiceButtonPrefab, _ui.ChoicesContainer);
        var btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = choice.choice;

        Button btn = btnObj.GetComponent<Button>();
        DialogueNode next = choice.nextNode;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        btn.onClick.AddListener(() => HandleChoice(next, choice));
        return btnObj;
    }

    private void HandleChoice(DialogueNode next, DialogueNode.DialogueChoice current)
    {
        string setFlag = current.dialogueFlags.SetFlagAfter;
        if (!string.IsNullOrEmpty(setFlag))
        {
            GameFlags.SetFlag(setFlag);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (next == null)
        {
            EndDialogue();
        }
        else
        {
            LoadNode(next);
        }
    }

    private void AdvanceToNextLine()
    {
        _currentLineIndex++;

        if (_currentLineIndex < _currentNode.lines.Count)
        {
            StartTyping(_currentNode.lines[_currentLineIndex]);
        }
        else
        {
            ShowChoices();
        }
    }

    private void StartTyping(string line)
    {
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private void PlayBlip(char letter)
    {
        if (letter == ' ' || !_audio.AudioSource || !_currentBlip) return;

        float blipVolume = (_mouseHoldTime >= HoldThreshold)
            ? _baseVolume * _fastTypeVolumeMultiplier
            : _baseVolume;

        _audio.AudioSource.pitch = Random.Range(0.95f, 1.05f);
        _audio.AudioSource.PlayOneShot(_currentBlip, blipVolume);
        _blipTimer = _blipCooldown;
    }

    private IEnumerator SetSelectionNextFrame(GameObject target)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(target);
    }
}