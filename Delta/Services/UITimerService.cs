namespace Delta.Services;

public class UITimerService
{
    public event Action? OnUpdate;

    private readonly TimeSpan period = TimeSpan.FromMilliseconds(100);
    private PeriodicTimer? timer;
    private CancellationTokenSource? cancellation;
    private Task? loop;

    public void Start()
    {
        if (loop is not null) return;

        cancellation = new CancellationTokenSource();
        timer = new PeriodicTimer(period);
        loop = Run(cancellation.Token);
    }

    private async Task Run(CancellationToken ct)
    {
        try
        {
            while (timer is not null && await timer.WaitForNextTickAsync(ct))
            {
                OnUpdate?.Invoke();
            }
        }
        catch (OperationCanceledException) { }
    }

    public async ValueTask DisposeAsync()
    {
        if (cancellation is null) return;

        cancellation.Cancel();
        if (loop is not null)
        {
            try { await loop; }
            catch (OperationCanceledException) { }
        }

        timer?.Dispose();
        cancellation.Dispose();
    }
}
