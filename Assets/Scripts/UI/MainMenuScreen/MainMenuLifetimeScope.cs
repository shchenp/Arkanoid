using MessagePipe;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI.MainMenuScreen
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameData _newGameData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PrefsManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.RegisterInstance(_newGameData);
            
            RegisterMessagePipe(builder);
        }
        
        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
        
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        }
    }
}