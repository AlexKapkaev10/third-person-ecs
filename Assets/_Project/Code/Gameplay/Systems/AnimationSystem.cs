using Project.Code.Gameplay.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Code.Gameplay.Systems
{
    public class AnimationSystem : ISystem
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private const float SpeedDampTime = 0.1f;

        public World World { get; set; }

        private Filter _filter;
        private Stash<InputComponent> _inputStash;
        private Stash<AnimatorComponent> _animatorStash;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<InputComponent>()
                .With<AnimatorComponent>()
                .Build();

            _inputStash = World.GetStash<InputComponent>();
            _animatorStash = World.GetStash<AnimatorComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputStash.Get(entity);
                ref var animator = ref _animatorStash.Get(entity);

                var targetSpeed = input.MoveDirection.magnitude;
                animator.Value.SetFloat(SpeedHash, targetSpeed, SpeedDampTime, deltaTime);
            }
        }

        public void Dispose()
        {
        }
    }
}