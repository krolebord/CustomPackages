namespace CustomPackages.Blazor.StateNotifiers;

public abstract class StateNotifier<TState> : IStateNotifier<TState>
{
    public abstract TState CurrentState { get; }

    public event Action? StateChanged;

    protected void NotifyStateChanged() => StateChanged?.Invoke();
}
