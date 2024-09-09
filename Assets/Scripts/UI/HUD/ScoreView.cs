using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class ScoreView : Viewer
    {
        [Inject]
        private void Construct(ISubscriber<ScoreUpdatedMessage> scoreSubscriber)
        {
            _subscription = scoreSubscriber.Subscribe(message => UpdateView(message.Score));
        }
    }
}