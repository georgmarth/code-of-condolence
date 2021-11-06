using UniRx;
using UnityEngine;

public class ClickEventManager : Singleton<ClickEventManager>
{
    public readonly Subject<Interactable> InteractableClicks = new Subject<Interactable>();
    public readonly Subject<Vector3> WorldClicks = new Subject<Vector3>();
}