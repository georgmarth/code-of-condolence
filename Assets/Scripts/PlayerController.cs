using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float Speed = 1f;

    public readonly Subject<Interactable> ArrivingAt = new Subject<Interactable>();

    private readonly SerialDisposable _serialDisposable = new SerialDisposable();

    private void Start()
    {
        ClickEventManager.Instance
            .InteractableClicks
            .Where(_ => CanInteract)
            .Subscribe(StartWalkingTo);

        ClickEventManager.Instance
            .WorldClicks
            .Where(_ => CanInteract)
            .Subscribe(StartWalkingTo);
        
        ArrivingAt.Subscribe(WhenArrivingAtInteractable);
    }

    private static bool CanInteract => 
        !DialogueInstance.Instance.DialogueRunner.IsDialogueRunning &&
        !GameStateManager.Instance.GameIsWon.Value &&
        !GameStateManager.Instance.AllOptionsAreExhausted.Value &&
        !GameStateManager.Instance.GameMenuIsShown.Value;

    private void WhenArrivingAtInteractable(Interactable interactable)
    {
        var dialogueRunner = DialogueInstance.Instance.DialogueRunner;
        if (interactable.scriptToLoad != null && !dialogueRunner.NodeExists(interactable.talkToNode))
        {
            dialogueRunner.Add(interactable.scriptToLoad);
        }

        dialogueRunner.StartDialogue(interactable.talkToNode);
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