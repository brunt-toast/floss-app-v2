namespace FlossApp.Application.Utils;

public class AsyncDelayedAction
{
    private readonly Func<Task> _task;
    private readonly int _delay;

    private CancellationTokenSource _cancellationTokenSource = new();

    public AsyncDelayedAction(Func<Task> task, int msDelay)
    {
        _task = task;
        _delay = msDelay;
    }

    public async Task TriggerImmediatelyAsync()
    {
        await _task();
    }

    public async Task TriggerWithDelayAsync()
    {
        try
        {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource = new CancellationTokenSource();

            await Task.Delay(_delay, _cancellationTokenSource.Token);
            await _task();
        }
        catch (OperationCanceledException) when (_cancellationTokenSource.IsCancellationRequested)
        {
            // ignored
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
        finally
        {
            await _cancellationTokenSource.CancelAsync();
        }
    }
}
