namespace CustomPackages.Blazor.StateNotifiers;

public class OpenStateNotifier<TState> : StateNotifier<TState>, IOpenStateNotifier<TState>
{
    protected TState state;

    public override TState CurrentState => state;

    public OpenStateNotifier(TState state)
    {
        this.state = state;
    }
    
    public void SetState(TState newState)
    {
        if (EqualityComparer<TState>.Default.Equals(state, newState))
        {
            return;
        }

        state = newState;
        NotifyStateChanged();
    }
}
