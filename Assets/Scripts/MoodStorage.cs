using System;
using UniRx;
using UnityEngine;
using Yarn.Unity;

public class MoodStorage : InMemoryVariableStorage
{
    public float MaximumMood = 100f;
    public IObservable<float> GetMoodAsObservable(string character)
    {
        return Observable.EveryUpdate().Select(_ => GetMood(character));
    }

    public float GetMood(string character)
    {
        return GetValue($"{character}Mood").AsNumber;
    }

    public void SetMood(string character, float value)
    {
        Debug.Log($"Set Mood {character} {value}");
        SetValue($"{character}Mood", Mathf.Clamp(value, 0, MaximumMood));
    }
    public bool GetSpriteState(string name)
    {
        return GetValue($"sprite{name}").AsBool;
    }
    public void SetSpriteState(string name, bool isVisible)
    {
        Debug.Log($"SetSpriteState {name} {isVisible}");
        SetValue($"sprite{name}", isVisible);
    }
    public IObservable<bool> GetSpriteStateAsObservable(string name)
    {
        return Observable.EveryUpdate().Select(_ => GetSpriteState(name));
    }
}