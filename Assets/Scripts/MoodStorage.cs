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
}