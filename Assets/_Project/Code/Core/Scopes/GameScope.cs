using Project.Code.Core.ECS;
using Project.Code.Gameplay.Systems;
using VContainer;
using VContainer.Unity;

namespace Project.Code.Core.Scopes
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EcsLoop>()
                .As<IEcsLoop>();
            
            builder.Register<InputSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            builder.Register<MoveSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<RotationSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<AnimationSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<CameraFollowSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}