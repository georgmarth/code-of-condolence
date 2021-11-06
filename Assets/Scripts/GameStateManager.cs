using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public IObservable<bool> GameIsWon { get; private set; }

    private void Awake()
    {
        GameIsWon = FindObjectsOfType<NPC>()
            .Select(GetMoodObservable)
            .CombineLatest()
            .Select(CheckWinCondition)
            .DistinctUntilChanged();

        GameIsWon
            .Where(gameIsWon => gameIsWon)
            .Subscribe(_ => Debug.Log("You win!"));
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