using UnityEngine;
using System.Collections.Generic;
using System;

public class InteractableDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float _interactRadius = 2f;
    [SerializeField] private float _interactAngle = 120f;
    [SerializeField] private LayerMask _interactableLayers;
    

    [Header("Detection Scan Thresholds")]
    [SerializeField] private float _moveThreshold = 0.1f;
    [SerializeField] private float _rotationThreshold = 2f;
    private Vector3 _lastPosition;
    private Quaternion _lastRotation;


    public GameObject BestTarget { get; private set; }
    public List<GameObject> AllTargetsInRange { get; private set; } = new List<GameObject>();
    public static event Action<GameObject> OnBestTargetChanged;
    private float _bestScore;
    private GameObject _previousTarget;

    void Start()
    {
        _lastPosition = transform.position;
        _lastRotation = transform.rotation;
    }

    void Awake()
    {
        _interactableLayers = LayerMask.GetMask("NPC", "Interactable");
    }

    public void Update()
    {
        ScanOnSignificantMovement();

        if (_previousTarget != BestTarget)
        {
            {
                OnBestTargetChanged?.Invoke(BestTarget);

                _previousTarget = BestTarget;
            }
        }
    }

    private void ScanOnSignificantMovement()
    {
        if (Vector3.Distance(transform.position, _lastPosition) > _moveThreshold ||
            Quaternion.Angle(transform.rotation, _lastRotation) > _rotationThreshold)
        {
            Scan();
            _lastPosition = transform.position;
            _lastRotation = transform.rotation;
        }
    }

    public void Scan()
    {
        _bestScore = float.MaxValue;
        BestTarget = null;
        AllTargetsInRange.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, _interactRadius, _interactableLayers);

        CalculateInteractionScores(hits);
    }

    private void CalculateInteractionScores(Collider[] hits)
    {
        foreach (Collider hit in hits)
        {
            GameObject obj = hit.gameObject;
            AllTargetsInRange.Add(obj);

            CalculateAngleAndDistance(obj, out float angle, out float distance);

            float score = AssignPoints(angle, distance);

            CalculateBestScore(obj, score);
        }
    }

    private void CalculateAngleAndDistance(GameObject obj, out float angle, out float distance)
    {
        Vector3 toTarget = (obj.transform.position - transform.position).normalized;
        angle = Vector3.Angle(transform.forward, toTarget);
        distance = Vector3.Distance(transform.position, obj.transform.position);
    }

    private float AssignPoints(float angle, float distance)
    {
        float angleBonus = angle <= _interactAngle / 2f ? 0f : 100f;
        float score = angleBonus + distance;
        return score;
    }

    private void CalculateBestScore(GameObject obj, float score)
    {
        if (score < _bestScore)
        {
            _bestScore = score;
            BestTarget = obj;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _interactRadius);

        Vector3 forward = transform.forward * _interactRadius;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-_interactAngle / 2f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(_interactAngle / 2f, Vector3.up);

        Vector3 leftRay = leftRayRotation * forward;
        Vector3 rightRay = rightRayRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRay);
        Gizmos.DrawRay(transform.position, rightRay);
    }
}