using Project.Components;
using Project.Gameplay.Services;
using Scellecs.Morpeh;

namespace Project.Systems
{
    public class InputSystem : ISystem
    {
        public World World { get; set; }
        
        private readonly IInputService _inputService;
        private Filter _filter;
        private Stash<InputComponent> _inputStash;

        public InputSystem(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<InputComponent>().Build();
            _inputStash = World.GetStash<InputComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputStash.Get(entity);
                input.MoveDirection = _inputService.MoveDirection;
            }
        }

        public void Dispose()
        {
            
        }
    }
}