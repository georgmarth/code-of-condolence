using System.Collections;
using UnityEngine;

namespace Assets.Scripts.YarnTest
{
    public class IncreaseMood : MonoBehaviour
    {
        // The dialogue runner that we want to attach the 'visited' function to
#pragma warning disable 0649
        [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
#pragma warning restore 0649

        void Start()
        {
            // Register a function on startup called "visited" that lets Yarn
            // scripts query to see if a node has been run before.
            dialogueRunner.AddFunction("increaseMood", 1, delegate (Yarn.Value[] parameters)
            {
                var amount = parameters[0].AsNumber;
            });
            dialogueRunner.AddFunction("decreaseMood", 1, delegate (Yarn.Value[] parameters)
            {
                var amount = parameters[0].AsNumber;
            });
        }
    }
}