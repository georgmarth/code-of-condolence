using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider NarrationSlider;
    public Slider SFXSlider;
    public Slider AmbienceSlider;

    private void Start()
    {
        MasterSlider.OnValueChangedAsObservable().Subscribe(value => AdjustFader(value, "Master"));
        MusicSlider.OnValueChangedAsObservable().Subscribe(value => AdjustFader(value, "Music"));
        NarrationSlider.OnValueChangedAsObservable().Subscribe(value => AdjustFader(value, "Narration"));
        SFXSlider.OnValueChangedAsObservable().Subscribe(value => AdjustFader(value, "SFX"));
        AmbienceSlider.OnValueChangedAsObservable().Subscribe(value => AdjustFader(value, "Ambience"));
    }

    private void AdjustFader(float value, string FaderName)
    {
        var attenuation = Mathf.Lerp(-80f, 0f, InverseSquare(value));
        Debug.Log($"set volume {FaderName} to {attenuation}");
        AudioMixer.SetFloat($"{FaderName}Vol", attenuation);
    }

    private static float InverseSquare(float value)
    {
        return 1f - ((1f - value) * (1f - value));
    }
}