using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class Platform : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private PlatformView _view;

        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }

        #region IPoolable Implementation
        public void OnDespawned()
        {
            //_registry.RemoveEnemy(this);
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            _view.Appear();
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        #endregion

        public void Die()
        {
            _view.Disappear(Dispose);
        }

        [Inject]
        public void Init(PlatformView view)
        {
            _view = view;
        }

        public class Factory : PlaceholderFactory<Platform>
        {
        }
    }
}
