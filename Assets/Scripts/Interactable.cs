using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform TargetPosition;
    public string interactableName = "";

    public string talkToNode = "";

    [Header("Optional")]
    public YarnProgram scriptToLoad;

}