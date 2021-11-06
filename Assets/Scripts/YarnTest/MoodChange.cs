using UnityEngine;
using Yarn.Unity;

namespace Assets.Scripts.YarnTest
{
    public class MoodChange : MonoBehaviour
    {
        void Start()
        {
            DialogueRunner dialogueRunner = DialogueInstance.Instance.DialogueRunner;
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
            var name = parameters[0];
            var amount = float.Parse(parameters[1]);
            IncreaseMood(name, amount);
            if (name == "all")
            {
                IncreaseMoodForAll(amount);
            }
            else
            {
                IncreaseMood(name, amount);
            }
        }
        public string GetMoodVariableName(string character)
        {
            return $"{name}Mood";
        }
        public void IncreaseMood(string character, float amount)
        {
            var storage = DialogueInstance.Instance.MoodStorage;
            var newValue = storage.GetMood(character) + amount;
            storage.SetMood(character, newValue);
        }

        public void DecreaseMood(string[] parameters)
        {
            var name = parameters[0];
            var amount = float.Parse(parameters[1]);
            if (name == "all")
            {
                DecreaseMoodForAll(amount);
            }
            else
            {
                DecreaseMood(name, amount);
            }
        }

        public void DecreaseMood(string character, float amount)
        {
            var storage = DialogueInstance.Instance.MoodStorage;
            var newValue = storage.GetMood(character) - amount;
            storage.SetMood(character, newValue);
        }

        void IncreaseMoodForAll(float amount)
        {
            var npcs = GetAllNpcs();
            foreach (var npc in npcs)
            {
                var name = npc.Interactable.interactableName;
                IncreaseMood(name, amount);
            }
        }
        void DecreaseMoodForAll(float amount)
        {
            var npcs = GetAllNpcs();
            foreach (var npc in npcs)
            {
                var name = npc.Interactable.interactableName;
                DecreaseMood(name, amount);
            }

        }
        NPC[] GetAllNpcs()
        {
            return FindObjectsOfType<NPC>();
        }
    }
}