using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    protected AudioSource source;

    [SerializeField]
    protected AudioClip opening;

    public void Play()
    {
        source.Stop();

        source.PlayOneShot(opening);
        source.PlayScheduled(AudioSettings.dspTime + opening.length);
    }

    public void Stop()
    {
        source.Stop();
    }

    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            source = GetComponent<AudioSource>();

            Play();
        }
    }
}
