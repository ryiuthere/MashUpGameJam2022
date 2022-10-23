using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip playerShoot;
    [SerializeField]
    protected AudioClip enemyShoot;
    [SerializeField]
    protected AudioClip hit;
    [SerializeField]
    protected AudioClip explosion;
    [SerializeField]
    protected AudioClip heal;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.PlayerShoot:
                audioSource.PlayOneShot(playerShoot);
                break;
            case SoundType.EnemyShoot:
                audioSource.PlayOneShot(enemyShoot);
                break;
            case SoundType.Hit:
                audioSource.PlayOneShot(hit);
                break;
            case SoundType.Explosion:
                audioSource.PlayOneShot(explosion);
                break;
            case SoundType.Heal:
                audioSource.PlayOneShot(heal);
                break;
            default:
                break;
        }
    }
}

public enum SoundType
{
    PlayerShoot,
    EnemyShoot,
    Hit,
    Explosion,
    Heal
}
