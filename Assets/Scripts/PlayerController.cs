using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float Speed = 1f;

    public readonly Subject<Interactable> ArrivingAt = new Subject<Interactable>();

    private readonly SerialDisposable _serialDisposable = new SerialDisposable();
    public Yarn.Unity.DialogueRunner dialogueRunner;

    private void Start()
    {
        ClickEventManager.Instance
            .InteractableClicks
            .Subscribe(StartWalkingTo);

        ClickEventManager.Instance
            .WorldClicks
            .Subscribe(StartWalkingTo);
        ArrivingAt.Subscribe(interactable =>
        {
            if (interactable.scriptToLoad != null)
            {
                dialogueRunner.Add(interactable.scriptToLoad);
            }
            dialogueRunner.StartDialogue(interactable.talkToNode);
        });

    }

    private void StartWalkingTo(Interactable interactable)
    {
        var targetPosition = interactable.TargetTransform.position;
        var destinationVector = GetDestinationVector(targetPosition);
        _serialDisposable.Disposable = DoMove(destinationVector)
            .OnComplete(() => ArrivingAt.OnNext(interactable))
            .AsDisposableTween();
    }

    private void StartWalkingTo(Vector3 targetPosition)
    {
        var destinationVector = GetDestinationVector(targetPosition);
        _serialDisposable.Disposable = DoMove(destinationVector)
            .AsDisposableTween();
    }

    private Tween DoMove(Vector3 destinationVector)
    {
        var position = transform.position;
        var distance = (position - destinationVector).magnitude;
        var moveTime = distance / Speed;
        return transform
            .DOMove(destinationVector, moveTime);
    }

    private Vector3 GetDestinationVector(Vector3 target)
    {
        var position = transform.position;
        return new Vector3(target.x, position.y, position.z);
    }
}