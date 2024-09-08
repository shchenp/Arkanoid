using System;
using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class LivesCounterView : Viewer
    {
        private IDisposable _subscription;
        
        [Inject]
        private void Construct(ISubscriber<LivesUpdatedMessage> enemyDiedSubscriber)
        {
            _subscription = enemyDiedSubscriber.Subscribe(message => UpdateView(message.Lives));
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}