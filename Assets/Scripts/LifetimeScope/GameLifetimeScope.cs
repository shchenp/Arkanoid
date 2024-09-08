using UnityEngine;
using VContainer;

namespace LifetimeScope
{
    public class GameLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] private PlayerController _playerController;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.Register<InputHandler>(Lifetime.Singleton);
            builder.RegisterInstance(_playerController)
                .AsImplementedInterfaces();
        }
    }
}