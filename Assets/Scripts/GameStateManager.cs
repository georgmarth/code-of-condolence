using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public IReadOnlyReactiveProperty<bool> GameIsWon { get; private set; }
    public IReadOnlyReactiveProperty<bool> AllOptionsAreExhausted { get; private set; }
    public readonly IReactiveProperty<bool> GameMenuIsShown = new ReactiveProperty<bool>(true);

    private void Awake()
    {
        GameIsWon = FindObjectsOfType<NPC>()
            .Select(GetMoodObservable)
            .CombineLatest()
            .Select(CheckWinCondition)
            .ToReadOnlyReactiveProperty(false);

        AllOptionsAreExhausted = Observable.EveryUpdate()
            .Select(_ => AreAllOptionsExhausted())
            .ToReadOnlyReactiveProperty();
            

        GameIsWon
            .Where(gameIsWon => gameIsWon)
            .Subscribe(_ => Debug.Log("You win!"));
    }

    private static bool AreAllOptionsExhausted()
    {
        return FindObjectsOfType<NPC>()
            .All(npc => DialogueInstance.Instance.MoodStorage.GetValue($"{npc.Interactable.interactableName}Completed")
                .AsBool);
    }

    private static IObservable<float> GetMoodObservable(NPC npc)
    {
        return DialogueInstance.Instance.MoodStorage.GetMoodAsObservable(npc.Interactable.interactableName);
    }

    private static bool CheckWinCondition(IList<float> moods)
    {
        return moods.All(mood => mood >= 100);
    }
}