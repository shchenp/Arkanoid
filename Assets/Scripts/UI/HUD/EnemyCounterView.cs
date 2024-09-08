using System;
using MessagePipe;
using Messages;
using VContainer;

namespace UI.HUD
{
    public class EnemyCounterView : Viewer
    {
        private IDisposable _subscription;
        
        [Inject]
        private void Construct(ISubscriber<EnemyCountUpdatedMessage> enemyCountSubscriber)
        {
            _subscription = enemyCountSubscriber.Subscribe(message => UpdateView(message.AliveCount, message.TotalCount));
        }

        private void UpdateView(int alive, int total)
        {
            _value.text = $"{alive} / {total}";
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}