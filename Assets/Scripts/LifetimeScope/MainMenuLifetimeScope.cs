using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace LifetimeScope
{
    public class MainMenuLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] private GameData _newGameData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.RegisterInstance(_newGameData);
        }
    }
}