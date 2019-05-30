using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class PlayerFallHandler : ITickable
    {
        private readonly PlayerInputState _inputState;
        private readonly PlayerView _view;
        private readonly Settings _settings;

        public PlayerFallHandler(PlayerView view, PlayerInputState inputState, Settings settings)
        {
            _view = view;
            _inputState = inputState;
            _settings = settings;
        }

        public void Tick()
        {
            if (_inputState.MoveDirection == EMoveDirection.None)
                return;

            if (!_view.IsDead && Mathf.Abs(_view.Velocity.y) > _settings.MinFallVelocity)
                Fall();
        }

        private void Fall()
        {
            _view.IsDead = true;
            Debug.Log("Fall");
        }

        [Serializable]
        public class Settings
        {
            public float MinFallVelocity;
        }
    }
}
