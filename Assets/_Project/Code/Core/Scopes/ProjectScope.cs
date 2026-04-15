using Project.Gameplay.Services;
using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Project.Code.Core
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
        }
    }
}