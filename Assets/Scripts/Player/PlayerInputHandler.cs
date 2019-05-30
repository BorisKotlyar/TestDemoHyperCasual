using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class PlayerInputHandler : ITickable
    {
        private readonly PlayerInputState _inputState;

        public PlayerInputHandler(PlayerInputState inputState)
        {
            _inputState = inputState;
        }

        public void Tick()
        {
            if (Input.anyKeyDown)
            {
                _inputState.MoveDirection = _inputState.MoveDirection == EMoveDirection.Right
                    ? EMoveDirection.Forward
                    : EMoveDirection.Right;
            }
        }
    }
}