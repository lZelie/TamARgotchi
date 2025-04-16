using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f; // durée du fade-out en secondes

    private AudioSource audioSource;
    private Coroutine fadeOutCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Lance le fade out avec la durée définie dans l'inspecteur.
    /// </summary>
    public void FadeOut()
    {
        if (fadeOutCoroutine != null)
            StopCoroutine(fadeOutCoroutine);

        fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
