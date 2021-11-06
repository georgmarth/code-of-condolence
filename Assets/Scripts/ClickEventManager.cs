using UniRx;

public class ClickEventManager : Singleton<ClickEventManager>
{
    public readonly Subject<ClickEvent> Clicks = new Subject<ClickEvent>();
}