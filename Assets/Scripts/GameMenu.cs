using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    public Canvas Canvas;
    public GameObject Letter;
    public Button SkipButton;
    public AudioSource Narrator;
    public Button AfterLetterText;

    private bool _letterWasShown;
    
    private void Start()
    {
        SetStartMenuActive(true);
        _letterWasShown = false;
        Letter.gameObject.SetActive(false);
        AfterLetterText.gameObject.SetActive(false);
        Music.Instance.SwitchMusic(Music.Instance.FuneralMarch);

        StartButton
            .OnClickAsObservable()
            .Subscribe(_ => PlayIntro());

        ExitButton.OnClickAsObservable()
            .Subscribe(_ => Application.Quit());

        Observable.EveryUpdate()
            .Select(_ => Input.GetKey(KeyCode.Escape))
            .DistinctUntilChanged()
            .Where(keyPressed => keyPressed)
            .Subscribe(_ => OnEscKeyPressed());
    }

    private void OnEscKeyPressed()
    {
        if (Canvas.gameObject.activeSelf && _letterWasShown)
        {
            SetStartMenuActive(false);
            return;
        }

        if (!Canvas.gameObject.activeSelf)
        {
            SetStartMenuActive(true);
        }

    }

    private void PlayIntro()
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
                .AsUnitObservable()
                .Merge(SkipButton.OnClickAsObservable().AsUnitObservable())
                .Take(1)
                .Subscribe(ShowAfterLetterText);
        }
        else
        {
            SetStartMenuActive(false);
        }
    }

    private void ShowAfterLetterText(Unit _)
    {
        AfterLetterText.gameObject.SetActive(true);
        AfterLetterText.OnClickAsObservable()
            .Take(1)
            .Subscribe(StartGame);
    }

    private void StartGame(Unit _)
    {
        _letterWasShown = true;
        Letter.SetActive(false);
        SetStartMenuActive(false);
        Music.Instance.SwitchMusic(Music.Instance.SadMusic);
    }

    private void SetStartMenuActive(bool value)
    {
        Canvas.gameObject.SetActive(value);
        GameStateManager.Instance.GameMenuIsShown.Value = value;
    }
}