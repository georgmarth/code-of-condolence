using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))]
public class NPC : MonoBehaviour
{
    public Slider moodSlider;
    public float StartingMood;
    public Interactable Interactable;

    private void Start()
    {
        Interactable = GetComponent<Interactable>();
        var interactableName = Interactable.interactableName;

        var instanceMoodStorage = DialogueInstance.Instance.MoodStorage;

        instanceMoodStorage.GetMoodAsObservable(interactableName)
            .DistinctUntilChanged()
            .Subscribe(UpdateSlider);

        Observable.NextFrame().Subscribe(_ => instanceMoodStorage.SetMood(interactableName, StartingMood));
    }

    private void UpdateSlider(float mood)
    {
        moodSlider.value = mood;
    }
}