using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer.Unity;

namespace Project.Code.Core.ECS
{
    public interface IEcsLoop : IInitializable, ITickable
    {
        
    }
    
    public class EcsLoop : IEcsLoop
    {
        private readonly World _world;
        private readonly IReadOnlyList<ISystem> _systems;
        private SystemsGroup _systemsGroup;

        public EcsLoop(World world, IReadOnlyList<ISystem> systems)
        {
            _world = world;
            _systems = systems;
        }
        
        public void Initialize()
        {
            _systemsGroup = _world.CreateSystemsGroup();
            
            foreach (var system in _systems)
            {
                _systemsGroup.AddSystem(system);
            }
            
            _world.AddSystemsGroup(0, _systemsGroup);
            _world.Commit();
        }

        public void Tick()
        {
            if (_world.IsDisposed)
            {
                return;
            }
            
            _world.Update(Time.deltaTime);
            _world.CleanupUpdate(Time.deltaTime);
        }
    }
}