using Project.Code.Core.Services;
using Project.Code.Gameplay.Services;
using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Project.Code.Core.Scopes
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var world = World.Default;
            
            if (world == null)
            {
                world = World.Create();
            }
            
            world.UpdateByUnity = false;
            builder.RegisterInstance(world);
            
            builder.RegisterEntryPoint<InputService>()
                .As<IInputService>();
            
            builder.Register<AssetBundleService>(Lifetime.Singleton)
                .As<IAssetBundleService>();
        }
    }
}