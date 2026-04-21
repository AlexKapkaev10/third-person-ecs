using Project.Code.Gameplay.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Code.Gameplay.Systems
{
    public class CameraFollowSystem : ISystem
    {
        public World World { get; set; }

        private Filter _targetFilter;
        private Filter _cameraFilter;

        private Stash<RotationComponent> _rotationStash;
        private Stash<CameraFollowComponent> _cameraStash;

        public void OnAwake()
        {
            _targetFilter = World.Filter
                .With<CameraTargetComponent>()
                .With<RotationComponent>()
                .Build();

            _cameraFilter = World.Filter
                .With<CameraFollowComponent>()
                .Build();

            _rotationStash = World.GetStash<RotationComponent>();
            _cameraStash = World.GetStash<CameraFollowComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            var targetEntity = _targetFilter.FirstOrDefault();
            if (World.IsDisposed(targetEntity))
            {
                return;
            }       

            var targetTransform = _rotationStash.Get(targetEntity).Transform;

            foreach (var cameraEntity in _cameraFilter)
            {
                ref var follow = ref _cameraStash.Get(cameraEntity);

                var desiredPosition = targetTransform.position + follow.Offset;

                follow.CameraTransform.position = Vector3.SmoothDamp(
                    follow.CameraTransform.position,
                    desiredPosition,
                    ref follow.Velocity,
                    follow.SmoothTime);

                follow.CameraTransform.LookAt(targetTransform);
            }
        }

        public void Dispose()
        {
            
        }
    }
}