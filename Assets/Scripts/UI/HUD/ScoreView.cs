using System;
using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class ScoreView : Viewer
    {
        private IDisposable _subscription;
        
        [Inject]
        private void Construct(ISubscriber<ScoreUpdatedMessage> enemyDiedSubscriber)
        {
            _subscription = enemyDiedSubscriber.Subscribe(message => UpdateView(message.Score));
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}