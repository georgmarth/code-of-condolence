using System.Collections;
using UnityEngine;
using UniRx;
using Yarn.Unity;

namespace Assets.Scripts.YarnTest
{
    public class MoodChange : MonoBehaviour
    {
        Interactable currentInteractable;
        //        // The dialogue runner that we want to attach the 'visited' function to
#pragma warning disable 0649
        [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
#pragma warning restore 0649
        public Yarn.Unity.InMemoryVariableStorage variableStorage;

        void Start()
        {
            PlayerController.Instance.ArrivingAt.Subscribe(interactable =>
            {
                currentInteractable = interactable;
                Debug.Log("arrived at " + interactable.interactableName);

            });
            dialogueRunner.AddCommandHandler(
                "increasemood",
                IncreaseMood
            );
            dialogueRunner.AddCommandHandler(
                "decreasemood",
                IncreaseMood
            );
            // Register a function on startup called "visited" that lets Yarn
            // scripts query to see if a node has been run before.
        }

        public void IncreaseMood(string[] parameters)
        {
            var amount = float.Parse(parameters[0]);
            var variableName = $"{currentInteractable.interactableName}Mood";
            Debug.Log($"increased mood by {amount} for {currentInteractable.interactableName}");
            var newValue = variableStorage.GetValue(variableName).AsNumber + amount;
            variableStorage.SetValue(variableName, newValue);
        }

        public void DecreaseMood(string[] parameters)
        {
            var amount = float.Parse(parameters[0]);
            var variableName = $"{currentInteractable.interactableName}Mood";
            Debug.Log($"decreased mood by {amount} for {currentInteractable.interactableName}");
            var newValue = variableStorage.GetValue(variableName).AsNumber - amount;
            variableStorage.SetValue(variableName, newValue);
        }
    }
}