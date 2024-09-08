using MessagePipe;
using VContainer;

namespace LifetimeScope
{
    public abstract class BaseLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PrefsManager>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            RegisterMessagePipe(builder);
        }

        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
        
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        }
    }
}