using UniRx;
using UnityEngine;

public class GameEndScreens : MonoBehaviour
{
    public Canvas GameWinScreen;
    public Canvas GameOverScreen;

    public AudioClip GameWinAudio;
    public AudioClip GameOverAudio;
    
    private void Start()
    {
        GameWinScreen.gameObject.SetActive(false);
        GameOverScreen.gameObject.SetActive(false);
        
        GameStateManager.Instance.GameIsWon
            .Where(isWon => isWon)
            .Take(1)
            .Subscribe(_ => ActivateGameWinScreen());

        GameStateManager.Instance.AllOptionsAreExhausted
            .Where(isOver => isOver)
            .Take(1)
            .Subscribe(_ => ActivateGameOverScreen());
    }

    private void ActivateGameWinScreen()
    {
        GameWinScreen.gameObject.SetActive(true);
        Music.Instance.SwitchMusic(GameWinAudio);
    }

    private void ActivateGameOverScreen()
    {
        GameOverScreen.gameObject.SetActive(true);
        Music.Instance.SwitchMusic(GameOverAudio);
    }
}