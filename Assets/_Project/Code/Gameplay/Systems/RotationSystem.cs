using Project.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Systems
{
    public class RotationSystem : ISystem
    {
        public World World { get; set; }

        private Filter _filter;
        private Stash<InputComponent> _inputStash;
        private Stash<RotationComponent> _transformStash;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<InputComponent>()
                .With<RotationComponent>()
                .Build();

            _inputStash = World.GetStash<InputComponent>();
            _transformStash = World.GetStash<RotationComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputStash.Get(entity);
                ref var rotationComponent = ref _transformStash.Get(entity);

                if (input.MoveDirection == Vector2.zero)
                    continue;

                var direction = new Vector3(input.MoveDirection.x, 0f, input.MoveDirection.y);
                var targetRotation = Quaternion.LookRotation(direction);
                rotationComponent.Transform.rotation = Quaternion.Lerp(
                    rotationComponent.Transform.rotation, 
                    targetRotation, 
                    rotationComponent.Speed * deltaTime);
            }
        }

        public void Dispose() { }
    }
}