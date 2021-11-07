using DG.Tweening;
using UniRx;
using UnityEngine;

public class Music : Singleton<Music>
{
    public AudioSource MusicSource;

    public AudioClip FuneralMarch;
    public AudioClip SadMusic;
    public AudioClip PartyMusic;
    public AudioClip WinningMusic;

    public float FadeDuration = .2f;

    private SerialDisposable _disposable = new SerialDisposable();
    
    public void SwitchMusic(AudioClip newClip)
    {
        if (MusicSource.isPlaying)
        {
            _disposable.Disposable = MusicSource.DOFade(0, FadeDuration)
                .OnComplete(() => StartNewMusic(newClip))
                .AsDisposableTween();
        }
        else
        {
            StartNewMusic(newClip);
        }
    }

    private void StartNewMusic(AudioClip newClip)
    {
        Debug.Log($"Start new music {newClip}");
        MusicSource.volume = 0;
        MusicSource.clip = newClip;
        MusicSource.Play();
        _disposable.Disposable = MusicSource.DOFade(1, FadeDuration).AsDisposableTween();
    }
}