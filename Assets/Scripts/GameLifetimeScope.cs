using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerController _playerController;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputHandler>(Lifetime.Singleton);
        builder.Register<PrefsManager>(Lifetime.Singleton)
            .AsImplementedInterfaces();
        builder.RegisterInstance(_playerController)
            .AsImplementedInterfaces();
        
        RegisterMessagePipe(builder);
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        builder.RegisterMessagePipe();
        
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
    }
}