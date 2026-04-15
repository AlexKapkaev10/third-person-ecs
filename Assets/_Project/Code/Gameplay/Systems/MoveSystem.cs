using Project.Components;
using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Systems
{
    public class MoveSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _filter;
        private Stash<MoveComponent> _moveStash;
        private Stash<InputComponent> _inputStash;
        private Stash<CharacterControllerComponent> _characterStash;

        public void OnAwake()
        {
            _filter = World.Filter
                .With<MoveComponent>()
                .With<InputComponent>()
                .With<CharacterControllerComponent>()
                .Build();
            
            _moveStash = World.GetStash<MoveComponent>();
            _inputStash = World.GetStash<InputComponent>();
            _characterStash = World.GetStash<CharacterControllerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var move = ref _moveStash.Get(entity);
                ref var input = ref _inputStash.Get(entity);
                ref var character = ref _characterStash.Get(entity);

                var direction = new Vector3(input.MoveDirection.x, 0, input.MoveDirection.y);
                character.value.SimpleMove(direction * move.Speed);
            }
        }

        public void Dispose()
        {
            
        }
    }
}