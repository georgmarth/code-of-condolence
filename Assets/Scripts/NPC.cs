using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class NPC : MonoBehaviour
{
    public Slider moodSlider;
    public float StartingMood;
    
    private void Start()
    {
        var interactableName = GetComponent<Interactable>().interactableName;

        var instanceMoodStorage = DialogueInstance.Instance.MoodStorage;

        instanceMoodStorage.GetMoodAsObservable(interactableName)
            .DistinctUntilChanged()
            .Subscribe(UpdateSlider);
        
        Observable.NextFrame().Subscribe(_ => instanceMoodStorage.SetMood(interactableName, StartingMood));
    }

    private void UpdateSlider(float mood)
    {
        Debug.Log($"Update Slider {mood}");
        moodSlider.value = mood;
    }
}