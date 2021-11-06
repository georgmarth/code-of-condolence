using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public Collider2D Collider;
    public readonly Subject<Unit> Clicked = new Subject<Unit>();
    public Interactable Interactable;

    private void Start()
    {
        Collider.OnMouseUpAsButtonAsObservable()
            .Subscribe(_ => OnClick());
    }

    private void OnClick()
    {
        ClickEventManager.Instance.Clicks.OnNext(new ClickEvent { ClickTarget = Interactable });
        Clicked.OnNext(Unit.Default);
    }
}