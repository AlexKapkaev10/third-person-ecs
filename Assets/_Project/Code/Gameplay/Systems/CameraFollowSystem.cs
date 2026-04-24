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

        private Stash<TransformComponent> _transformStash;
        private Stash<CameraFollowComponent> _cameraStash;

        public void OnAwake()
        {
            _targetFilter = World.Filter
                .With<CameraTargetComponent>()
                .With<TransformComponent>()
                .Build();

            _cameraFilter = World.Filter
                .With<CameraFollowComponent>()
                .Build();

            _transformStash = World.GetStash<TransformComponent>();
            _cameraStash = World.GetStash<CameraFollowComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            var targetEntity = _targetFilter.FirstOrDefault();
            if (World.IsDisposed(targetEntity))
            {
                return;
            }

            var targetTransform = _transformStash.Get(targetEntity).Value;

            foreach (var cameraEntity in _cameraFilter)
            {
                ref var follow = ref _cameraStash.Get(cameraEntity);

                var desiredPosition = targetTransform.position + follow.Offset;
                var positionT = 1f - Mathf.Exp(-follow.PositionSmoothness * deltaTime);
                follow.CameraTransform.position = Vector3.Lerp(
                    follow.CameraTransform.position,
                    desiredPosition,
                    positionT);

                var virtualCameraPosition = targetTransform.position 
                    + new Vector3(0f, follow.Offset.y, follow.Offset.z);
                var horizontal = targetTransform.position - virtualCameraPosition;
                horizontal.y = 0f;

                if (horizontal.sqrMagnitude < 0.0001f)
                {
                    continue;
                }
                
                var yaw = Quaternion.LookRotation(horizontal).eulerAngles.y;
                var desiredRotation = Quaternion.Euler(follow.Pitch, yaw, 0f);
                var rotationT = 1f - Mathf.Exp(-follow.RotationSmoothness * deltaTime);
                follow.CameraTransform.rotation = Quaternion.Slerp(
                    follow.CameraTransform.rotation,
                    desiredRotation,
                    rotationT);
            }

        }


        public void Dispose()
        {
            
        }
    }
}