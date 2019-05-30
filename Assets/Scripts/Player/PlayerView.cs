using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class PlayerView
    {
        private readonly Rigidbody _rigidbody;

        [Inject] private readonly SignalBus _signalBus;

        private bool _isDead;
        public bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value; 
                if (_isDead)
                    _signalBus.Fire<PlayerFallSignal>();
            }
        }

        public PlayerView(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public Vector3 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.position = value;
        }

        public Vector3 Velocity => _rigidbody.velocity;

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
        }
    }
}