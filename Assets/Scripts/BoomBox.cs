using UnityEngine;

public class BoomBox : MonoBehaviour
{
    public AudioClip MusicLevelTwo;

    private void Start()
    {
        Music.Instance.SwitchMusic(MusicLevelTwo);
    }
}