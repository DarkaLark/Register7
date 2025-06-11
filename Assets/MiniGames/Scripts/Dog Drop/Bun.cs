using UnityEngine;


public class Bun : MonoBehaviour
{
    private Transform _backBun;
    [SerializeField] Transform _frontBun;
    [SerializeField] int _moveSpeed = 1000;
    private Vector3 _newPosition = new();

    enum TravelingDirection { Left, Right }
    private TravelingDirection _currentDirection;

    void Start()
    {
        _backBun = transform;
        RandomizeStartPosition();

        _currentDirection = StartingDirection();

        _newPosition = new Vector3(_moveSpeed, 0, 0);
    }

    private void RandomizeStartPosition()
    {
        RectTransform bunRect = GetComponent<RectTransform>();
        float canvasWidth = ((RectTransform)bunRect.parent).rect.width;

        float randomX = Random.Range(-canvasWidth / 2f, canvasWidth / 2f);
        bunRect.anchoredPosition = new Vector2(randomX, bunRect.anchoredPosition.y);
    }

    void Update()
    {
        _frontBun.position = _backBun.position;

        if (_currentDirection == TravelingDirection.Right)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        TouchedWall();
    }

    private TravelingDirection StartingDirection()
    {
        if (Random.Range(0, 2) == 1)
        {
            return TravelingDirection.Right;
        }

        return TravelingDirection.Left;
    }

    private void MoveRight()
    {
        _backBun.position += -_newPosition * Time.deltaTime;
    }

    private void MoveLeft()
    {
        _backBun.position += _newPosition * Time.deltaTime;
    }
    private void TouchedWall()
    {
        if (_backBun.position.x <= 0)
        {
            _currentDirection = TravelingDirection.Left;
        }
        else if (_backBun.position.x >= Screen.width)
        {
            _currentDirection = TravelingDirection.Right;
        }
    }
}