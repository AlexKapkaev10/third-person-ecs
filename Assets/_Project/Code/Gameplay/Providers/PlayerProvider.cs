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
        
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;

        protected override void Initialize()
        {
            Move();
            Rotate();
            Input();
            CharacterController();
            CameraTarget();
        }

        private void Move()
        {
            World.Default.GetStash<MoveComponent>()
                .Add(Entity)
                .Speed = _moveSpeed;
        }

        private void Rotate()
        {
            ref var  rotationComponent = ref World.Default
                .GetStash<RotationComponent>()
                .Add(Entity);
            
            rotationComponent.Speed = _rotationSpeed;
            rotationComponent.Transform = _transform;
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