using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Project.Code.Gameplay.Services
{
    public interface IInputService : IInitializable, IDisposable
    {
        Vector2 MoveDirection { get; }
    }
    
    public class InputService : IInputService
    {
        private readonly InputActions _inputActions = new();
        public Vector2 MoveDirection { get; private set; }

        public void Initialize()
        {
            _inputActions.Enable();
            _inputActions.Player.Enable();
            
            PlayerSubscribe();
        }

        private void PlayerSubscribe()
        {
            _inputActions.Player.Move.performed += Move;
            _inputActions.Player.Move.canceled += Move;
            _inputActions.Player.Enable();
        }

        private void PlayerUnsubscribe()
        {
            _inputActions.Player.Move.performed -= Move;
            _inputActions.Player.Move.canceled -= Move;
            _inputActions.Player.Disable();
        }

        private void Move(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            PlayerUnsubscribe();
            
            _inputActions.Dispose();
        }
    }
}