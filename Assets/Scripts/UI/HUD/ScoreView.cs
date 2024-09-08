using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class ScoreView : Viewer
    {
        [Inject]
        private void Construct(ISubscriber<ScoreUpdatedMessage> enemyDiedSubscriber)
        {
            _subscription = enemyDiedSubscriber.Subscribe(message => UpdateView(message.Score));
        }
    }
}