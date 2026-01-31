using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
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

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    private void Start()
    {
        music.Play();
    }
}
