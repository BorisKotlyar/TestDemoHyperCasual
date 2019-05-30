using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class FXView : MonoBehaviour
    {
        [SerializeField] private Transform _cachedTransform;
        [SerializeField] private ParticleSystem _particleSystem;

        private Vector3 _position;
        private bool _isRun;

        [Inject]
        public FX Data
        {
            get; set;
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _cachedTransform.position = new Vector3(_position.x, 0, _position.z);
            }
        }

        public void Appear()
        {
            _particleSystem.Clear();
            _particleSystem.Play();

            _isRun = true;
        }

        protected void LateUpdate()
        {
            if (!_isRun)
                return;

            if (!_particleSystem.IsAlive())
            {
                _isRun = false;
                Data.Dispose();
            }
        }
    }
}