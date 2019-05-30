using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class Player : MonoBehaviour
    {
        private PlayerView _view;
        private PlayerInputState _inputState;
        private SignalBus _signalBus;

        [Inject]
        public void Init(PlayerView view, PlayerInputState inputState, SignalBus signalBus)
        {
            _view = view;
            _inputState = inputState;
            _signalBus = signalBus;
        }

        public Vector3 Position
        {
            get { return _view.Position; }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_inputState.MoveDirection == EMoveDirection.None)
                return;

            _signalBus.Fire<PlatformCollisionSignal>();
        }
    }
}
