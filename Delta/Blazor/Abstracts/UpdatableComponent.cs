using Delta.Services;
using Microsoft.AspNetCore.Components;

namespace Delta.Blazor.Abstracts;

public class UpdatableComponent : ComponentBase, IDisposable
{
    private bool disposed;
    [Inject] public UITimerService Timer { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        //Timer.OnUpdate += OnTick;
    }
    private void OnTick()
    {
        if (disposed) return;

        _ = InvokeAsync(async () =>
        {
            await OnUITick();
            StateHasChanged();
        });
    }
    protected virtual Task OnUITick() => Task.CompletedTask;
    public void Dispose()
    {
        disposed = true;
        //Timer.OnUpdate -= OnTick;
    }
}
