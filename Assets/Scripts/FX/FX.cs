using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class FX : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private FXView _view;

        public Vector3 Position
        {
            get => _view.Position;
            set => _view.Position = value;
        }

        #region IPoolable Implementation
        public void OnDespawned()
        {
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

        [Inject]
        public void Init(FXView view)
        {
            _view = view;
        }

        public class Factory : PlaceholderFactory<FX>
        {
        }
    }
}
