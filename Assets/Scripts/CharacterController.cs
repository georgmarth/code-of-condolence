using DG.Tweening;
using UniRx;

public class CharacterController : Singleton<CharacterController>
{
    public float Speed = 1f;
    
    public readonly Subject<Interactable> ArrivingAt = new Subject<Interactable>();

    private readonly SerialDisposable _serialDisposable = new SerialDisposable();
    
    private void Start()
    {
        ClickEventManager.Instance
            .Clicks
            .Select(evt => evt.ClickTarget)
            .Subscribe(StartWalking);
    }

    private void StartWalking(Interactable interactable)
    {
        var targetPosition = interactable.TargetPosition.position;
        var distance = (transform.position - targetPosition).magnitude;
        var moveTime = distance / Speed;
        _serialDisposable.Disposable = transform.DOMove(targetPosition, moveTime)
            .OnComplete(() => ArrivingAt.OnNext(interactable))
            .AsDisposableTween();
    }
}