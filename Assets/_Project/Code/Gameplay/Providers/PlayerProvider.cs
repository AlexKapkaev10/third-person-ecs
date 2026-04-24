using Project.Code.Gameplay.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Project.Code.Gameplay.Providers
{
    public class PlayerProvider : EntityProvider
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;

        protected override void Initialize()
        {
            Transform();
            Animator();
            Move();
            Rotate();
            Input();
            CharacterController();
            CameraTarget();
        }

        private void Transform()
        {
            World.Default.GetStash<TransformComponent>()
                .Add(Entity)
                .Value = _transform;
        }

        private void Animator()
        {
            World.Default.GetStash<AnimatorComponent>()
                .Add(Entity)
                .Value = _animator;
        }

        private void Move()
        {
            World.Default.GetStash<MoveComponent>()
                .Add(Entity)
                .Speed = _moveSpeed;
        }

        private void Rotate()
        {
            World.Default.GetStash<RotationComponent>()
                .Add(Entity)
                .Speed = _rotationSpeed;
        }

        private void Input()
        {
            World.Default.GetStash<InputComponent>()
                .Add(Entity);
        }

        private void CharacterController()
        {
           World.Default.GetStash<CharacterControllerComponent>()
               .Add(Entity)
               .Controller = _characterController;
        }
        
        private void CameraTarget()
        {
            World.Default.GetStash<CameraTargetComponent>()
                .Add(Entity);
        }
    }
}