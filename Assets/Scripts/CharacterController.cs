using DG.Tweening;
using UniRx;

public class CharacterController : Singleton<CharacterController>
{
    public float Speed = 1f;
    public Yarn.Unity.DialogueRunner dialogueRunner;


    public readonly Subject<Interactable> ArrivingAt = new Subject<Interactable>();

    private readonly SerialDisposable _serialDisposable = new SerialDisposable();

    private void Start()
    {
        ClickEventManager.Instance
            .Clicks
            .Select(evt => evt.ClickTarget)
            .Subscribe(StartWalking);
        ArrivingAt.Subscribe(interactable =>
        {
            if (interactable.scriptToLoad != null)
            {
                dialogueRunner.Add(interactable.scriptToLoad);
            }
            dialogueRunner.StartDialogue(interactable.talkToNode);
        });
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