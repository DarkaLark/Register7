using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float _despawnTimer = 3f;

    [SerializeField] AudioClip _popAudio;
    private AudioSource _audio;

    void Awake()
    {
        _audio = FindFirstObjectByType<AudioSource>();   
    }

    void Update()
    {
        _despawnTimer -= Time.deltaTime;
        if (_despawnTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        if (_audio != null)
        {
            _audio.pitch = Random.Range(.95f, 1.05f);
            _audio.PlayOneShot(_popAudio);
        }

        Destroy(gameObject);
    }
}