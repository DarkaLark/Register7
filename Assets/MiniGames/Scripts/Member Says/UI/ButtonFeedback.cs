using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonFeedback : MonoBehaviour
{
    [SerializeField] private PossibleItemsGameEvent _onPlayerResponse;

    [SerializeField] private List<Button> _buttons;

    private float _waitTime = 0.15f;

    void OnEnable()
    {
        _onPlayerResponse.Register(ButtonReact);
    }

    void OnDisable()
    {
        _onPlayerResponse.Unregister(ButtonReact);
    }

    private void ButtonReact(PossibleItems item)
    {
        foreach (Button btn in _buttons)
        {
            if (btn.name == item.ToString())
            {
                var colors = btn.colors;
                Color originalColor = btn.image.color;
                btn.image.color = colors.pressedColor;

                StartCoroutine(RestoreButtonColor(btn, originalColor));
            }
        }
    }
    private IEnumerator RestoreButtonColor(Button btn, Color originalColor)
    {
        yield return new WaitForSeconds(_waitTime);
        btn.image.color = originalColor;
    }
}