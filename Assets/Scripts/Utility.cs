using System;
using DG.Tweening;
using UniRx;

public static class Utility
{
    public static IDisposable AsDisposableTween(this Tween tween)
    {
        return Disposable.Create(() => tween.Kill());
    }
}