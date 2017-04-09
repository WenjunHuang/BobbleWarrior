using UnityEngine;
using UnityEngine.Assertions;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    #region Game Audio Clips

    public AudioClip GunFire;
    public AudioClip UpgradedGunFire;
    public AudioClip Hurt;
    public AudioClip AlienDeath;
    public AudioClip MarineDeath;
    public AudioClip Victory;
    public AudioClip ElevatorArrived;
    public AudioClip PowerUpPickup;
    public AudioClip PowerUpAppear;

    #endregion

    private AudioSource _soundEffectAudio;

    public void Start()
    {
        SetSingleton();
        InitializeSoundEffectAudio();
    }

    private void SetSingleton()
    {
        Assert.IsNull(Instance);
        Instance = this;
    }

    private void InitializeSoundEffectAudio()
    {
        var sources = GetComponents<AudioSource>();
        foreach (var source in sources)
        {
            if (source.clip == null)
            {
                _soundEffectAudio = source;
                break;
            }
        }
    }

    public void Update()
    {
    }

    public void PlayOneShot(AudioClip clip)
    {
        _soundEffectAudio.PlayOneShot(clip);
    }
}