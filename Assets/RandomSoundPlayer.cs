using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private List<RandomPitchPlayer> soundPlayers = new List<RandomPitchPlayer>();
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float maxInterval = 3f;
    [SerializeField] private bool playOnStart = true;

    private Coroutine playRoutine;

    private void Start()
    {
        if (playOnStart)
            StartPlaying();
    }

    public void StartPlaying()
    {
        if (playRoutine == null)
            playRoutine = StartCoroutine(PlayLoop());
    }

    public void StopPlaying()
    {
        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);
            playRoutine = null;
        }
    }

    private IEnumerator PlayLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            PlayRandomSound();
        }
    }

    public void PlayRandomSound()
    {
        if (soundPlayers == null || soundPlayers.Count == 0)
        {
            Debug.LogWarning("No RandomPitchPlayers assigned!");
            return;
        }

        int index = Random.Range(0, soundPlayers.Count);
        soundPlayers[index].Play();
    }
}
