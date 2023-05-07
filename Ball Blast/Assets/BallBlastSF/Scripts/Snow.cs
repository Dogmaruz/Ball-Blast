using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        if (FindObjectOfType<Snow>()._particleSystem.isPlaying) return;

        _particleSystem.Play();

        DontDestroyOnLoad(_particleSystem);
    }
}
