using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class PlayerMoveHandler : IInitializable, IDisposable, ITickable
    {
        private readonly PlayerView _view;
        private readonly PlayerInputState _inputState;
        private readonly Settings _settings;

        public PlayerMoveHandler(
            PlayerInputState inputState, 
            PlayerView view, 
            Settings settings)
        {
            _inputState = inputState;
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _inputState.OnChangeDirection += OnChangeDirection;
        }

        public void Dispose()
        {
            _inputState.OnChangeDirection -= OnChangeDirection;
        }

        public void Tick()
        {
            if (_view.IsDead)
                return;

            switch (_inputState.MoveDirection)
            {
                case EMoveDirection.Forward:
                    _view.SetVelocity(Vector3.forward * _settings.MoveSpeed);
                    break;

                case EMoveDirection.Right:
                    _view.SetVelocity(Vector3.right * _settings.MoveSpeed);
                    break;
            }
        }

        private void OnChangeDirection()
        {
            if (_view.IsDead)
                return;

            _view.Stop();
        }

        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
        }
    }
}
