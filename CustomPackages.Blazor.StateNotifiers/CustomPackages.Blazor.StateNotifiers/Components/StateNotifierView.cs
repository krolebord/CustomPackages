using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CustomPackages.Blazor.StateNotifiers.Components;

public class StateNotifierView<TNotifier, TState> : ComponentBase, IDisposable
    where TNotifier : IStateNotifier<TState>
{
    [Inject]
    public TNotifier Notifier { get; set; } = default!;

    [Parameter]
    public RenderFragment<StateNotifierViewContext>? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (ChildContent is not null)
        {
            builder.AddContent(0, ChildContent.Invoke(new(Notifier, Notifier.CurrentState)));
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Notifier.StateChanged += NotifierOnStateChanged;
    }

    private void NotifierOnStateChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        Notifier.StateChanged -= NotifierOnStateChanged;
    }

    // ReSharper disable NotAccessedPositionalProperty.Global
    public record StateNotifierViewContext(
        TNotifier Notifier,
        TState State
    );
    // ReSharper restore NotAccessedPositionalProperty.Global
}
