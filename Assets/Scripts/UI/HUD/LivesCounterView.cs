using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class LivesCounterView : Viewer
    {
        [Inject]
        private void Construct(ISubscriber<LivesUpdatedMessage> enemyDiedSubscriber)
        {
            _subscription = enemyDiedSubscriber.Subscribe(message => UpdateView(message.Lives));
        }
    }
}