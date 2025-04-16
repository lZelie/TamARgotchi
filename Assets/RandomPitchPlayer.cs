using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchPlayer : MonoBehaviour
{
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (audioSource.clip == null)
        {
            Debug.LogWarning("No audio clip assigned to the AudioSource.");
            return;
        }

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
