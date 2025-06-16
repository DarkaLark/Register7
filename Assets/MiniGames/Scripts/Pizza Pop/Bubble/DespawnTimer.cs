using UnityEngine;

public class DespawmTimer : MonoBehaviour
{
    [SerializeField] private float _despawnTimer = 3f;

    void Update()
    {
        _despawnTimer -= Time.deltaTime;
        if (_despawnTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}