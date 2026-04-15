using Project.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Project.Providers
{
    public class PlayerProvider : EntityProvider
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private CharacterController _characterController;

        protected override void Initialize()
        {
            Debug.Log("PlayerProvider Initialize called");
            var stash = World.Default.GetStash<MoveComponent>();
            ref var move = ref  stash.Add(Entity);
            move.Speed = _speed;
            
            World.Default.GetStash<InputComponent>().Add(Entity);
            
            var ccStash = World.Default.GetStash<CharacterControllerComponent>();
            ref var cc = ref ccStash.Add(Entity);
            cc.value = _characterController;
        }
    }
}