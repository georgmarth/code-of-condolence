using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class SpriteTracker : MonoBehaviour
{
    MoodStorage moodStorage;
    // Use this for initialization
    void Start()
    {
        moodStorage = DialogueInstance.Instance.MoodStorage;
        DialogueRunner dialogueRunner = DialogueInstance.Instance.DialogueRunner;
        dialogueRunner.AddCommandHandler(
            "showsprite",
            ShowSprite
        );
        dialogueRunner.AddCommandHandler(
            "hidesprite",
            HideSprite
        );
        dialogueRunner.AddFunction("spriteIsVisible", 1, delegate (Yarn.Value[] parameters)
        {
            var spriteName = parameters[0].AsString;
            return moodStorage.GetSpriteState(spriteName);
        });
        dialogueRunner.AddCommandHandler("setCompleted", SetCompleted);
    }
    void ShowSprite(string[] parameters)
    {
        var name = parameters[0];
        moodStorage.SetSpriteState(name, true);
    }
    void HideSprite(string[] parameters)
    {
        var name = parameters[0];
        moodStorage.SetSpriteState(name, false);
    }

    void SetCompleted(string[] parameters)
    {
        var name = parameters[0];
        moodStorage.SetCompleted(name);
    }

}
