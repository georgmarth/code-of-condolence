using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    public Canvas Canvas;

    private void Start()
    {
        StartButton
            .OnClickAsObservable()
            .Subscribe(_ => SetStartMenuActive(false));

        ExitButton.OnClickAsObservable()
            .Subscribe(_ => Application.Quit());

        Observable.EveryUpdate()
            .Select(_ => Input.GetKey(KeyCode.Escape))
            .DistinctUntilChanged()
            .Where(keyPressed => keyPressed)
            .Subscribe(_ => SetStartMenuActive(true));
    }

    private void SetStartMenuActive(bool value)
    {
        Canvas.gameObject.SetActive(value);
        GameStateManager.Instance.GameMenuIsShown.Value = value;
    }
}