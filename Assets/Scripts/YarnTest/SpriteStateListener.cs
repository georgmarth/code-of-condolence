using System.Collections;
using UnityEngine;
using UniRx;

public class SpriteStateListener : MonoBehaviour
{
    public GameObject SpriteObject;
    public string SpriteId;
    public bool InitialState;

    // Use this for initialization
    void Start()
    {
        var storage = DialogueInstance.Instance.MoodStorage;
        Observable.NextFrame().Subscribe(_ => storage.SetSpriteState(SpriteId, InitialState));
        storage.GetSpriteStateAsObservable(SpriteId).DistinctUntilChanged().Subscribe(isVisible => SpriteObject.SetActive(isVisible));
    }
}
