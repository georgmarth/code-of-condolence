using JetBrains.Annotations;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public Collider2D Collider;
    public readonly Subject<Unit> Clicked = new Subject<Unit>();
    
    [CanBeNull, Header("Optional")]
    public Interactable Interactable;

    private void Start()
    {
        Collider.OnMouseUpAsButtonAsObservable()
            .Subscribe(_ => OnClick());
    }

    private static Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnClick()
    {
        if (Interactable == null)
            ClickEventManager.Instance.WorldClicks.OnNext(GetMousePosition());
        else
            ClickEventManager.Instance.InteractableClicks.OnNext(Interactable);
        Clicked.OnNext(Unit.Default);
    }
}