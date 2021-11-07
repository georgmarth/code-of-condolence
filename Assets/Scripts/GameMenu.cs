using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    public Canvas Canvas;
    public GameObject Letter;
    public AudioSource Narrator;

    private bool _letterWasShown;
    
    private void Start()
    {
        SetStartMenuActive(true);
        _letterWasShown = false;
        Letter.gameObject.SetActive(false);

        StartButton
            .OnClickAsObservable()
            .Subscribe(_ => StartGame());

        ExitButton.OnClickAsObservable()
            .Subscribe(_ => Application.Quit());

        Observable.EveryUpdate()
            .Select(_ => Input.GetKey(KeyCode.Escape))
            .DistinctUntilChanged()
            .Where(keyPressed => keyPressed)
            .Subscribe(_ => SetStartMenuActive(true));
    }

    private void StartGame()
    {
        if (!_letterWasShown)
        {
            Letter.SetActive(true);
            Narrator.Play();
            Observable.EveryUpdate()
                .Select(_ => Narrator.isPlaying)
                .Skip(1)
                .DistinctUntilChanged()
                .Where(isPlaying => !isPlaying)
                .Take(1)
                .Subscribe(_ =>
                {
                    _letterWasShown = true;
                    Letter.SetActive(false);
                    SetStartMenuActive(false);
                });
        }
        else
        {
            SetStartMenuActive(false);
        }
    }

    private void SetStartMenuActive(bool value)
    {
        Canvas.gameObject.SetActive(value);
        GameStateManager.Instance.GameMenuIsShown.Value = value;
    }
}