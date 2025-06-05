using UnityEngine;

public class FlagZoneTiggered : MonoBehaviour
{
    [SerializeField] private string _flagToSet;
    [SerializeField] private bool _onlyOnce = true;

    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || (_hasTriggered && _onlyOnce)) return;

        GameFlags.SetFlag(_flagToSet);
        _hasTriggered = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.8f, 0f, 0.25f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }
} 