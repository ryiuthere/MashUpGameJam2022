using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
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
        source = GetComponent<AudioSource>();

        // Temporary - this will be removed once we have a title screen plus its music
        Play();
    }
}
