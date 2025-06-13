using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hotdog : MonoBehaviour
{
    private Transform _hotdog;
    
    [SerializeField] int _maxDropSpeed = 10;
    [SerializeField] int _startingDropSpeed = 4;
    [SerializeField] int _gravity = 1000;
    private int _currentDropSpeed = 0;

    [Space(5)]
    [SerializeField] private GameObject _bun;
    [SerializeField] private GameObject _backButton;

    [SerializeField] private GameEvent _onHotdogDrop;

    private float _speedTimer = .2f;
    private bool _drop = false;
    private bool _weinerTouchingBun = false;
    private float _outOfBoundsOffset = -100f;

    void OnEnable()
    {
        Debug.Log("OnEnable called in: Hotdog");
        _onHotdogDrop.Register(DropHotdog);
    }

    void OnDisable()
    {
        _onHotdogDrop.Unregister(DropHotdog);
    }

    void Start()
    {
        _hotdog = transform;
        _currentDropSpeed = _startingDropSpeed;
    }

    void Update()
    {
        if (_drop && !_weinerTouchingBun)
        {
            Fall();
            AccelerateHotdogSpeed();
        }  
    }

    void DropHotdog()
    {
        _drop = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _bun)
        {
            _weinerTouchingBun = true;
            _drop = false;
            _currentDropSpeed = 0;
            transform.SetParent(other.transform, true);
            _backButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_backButton);
        }
    }

    private void Fall()
    {
        _hotdog.position += Vector3.down * (_currentDropSpeed * _gravity) * Time.deltaTime;
        if (_bun.transform.position.y + _outOfBoundsOffset > _hotdog.position.y)
        {
            TextMeshProUGUI backButtonText = _backButton.GetComponentInChildren<TextMeshProUGUI>();
            backButtonText.text = "Wiener On The Floor!";
            
            _backButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_backButton);
            Destroy(gameObject);
        }

    }

    private void AccelerateHotdogSpeed()
    {
        if (_currentDropSpeed < _maxDropSpeed)
        {
            _speedTimer -= Time.deltaTime;
            if (_speedTimer <= 0f)
            {
                _currentDropSpeed++;
                _speedTimer = .2f;
            }
        }
    }
}
