namespace CustomPackages.Blazor.StateNotifiers;

public interface IStateNotifier<out TState>
{
    public TState CurrentState { get; }

    public event Action StateChanged;
}
