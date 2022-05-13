namespace CustomPackages.Blazor.StateNotifiers;

public interface IOpenStateNotifier<TState> : IStateNotifier<TState>
{
    public void SetState(TState state);
}
