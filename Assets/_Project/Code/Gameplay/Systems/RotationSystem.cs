using Project.Code.Gameplay.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Code.Gameplay.Systems
{
    public class RotationSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        private Stash<InputComponent> _inputStash;
        private Stash<RotationComponent> _rotationStash;
        private Stash<TransformComponent> _transformStash;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<InputComponent>()
                .With<RotationComponent>()
                .With<TransformComponent>()
                .Build();

            _inputStash = World.GetStash<InputComponent>();
            _rotationStash = World.GetStash<RotationComponent>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputStash.Get(entity);
                ref var rotation = ref _rotationStash.Get(entity);
                ref var transform = ref _transformStash.Get(entity);

                if (input.MoveDirection == Vector2.zero)
                {
                    continue;
                }

                var direction = new Vector3(input.MoveDirection.x, 0f, input.MoveDirection.y);
                var targetRotation = Quaternion.LookRotation(direction);

                transform.Value.rotation = Quaternion.Lerp(
                    transform.Value.rotation, 
                    targetRotation, 
                    rotation.Speed * deltaTime);
            }
        }

        public void Dispose() { }
    }
}