using UnityEngine;

public class LoadBGSound : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundSound;

    private void Awake()
    {
        if (FindObjectOfType<LoadBGSound>()._backgroundSound.isPlaying) return;
        
        _backgroundSound.Play();

        DontDestroyOnLoad(_backgroundSound);
    }
}
