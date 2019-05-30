using TestDemo.Animations;
using UnityEngine;
using Zenject;

namespace TestDemo.UI
{
    public class UIDeathHandler : MonoBehaviour
    {
        [SerializeField] private DOAnimation _animation;

        [Inject] private readonly SignalBus _signalBus;

        protected void OnEnable()
        {
            _signalBus.Subscribe<PlayerFallSignal>(OnDeath);

            _animation.Reset();
            _animation.Play();
        }

        protected void OnDisable()
        {
            _signalBus.Unsubscribe<PlayerFallSignal>(OnDeath);
        }

        private void OnDeath(PlayerFallSignal obj)
        {
            _animation.Rewind();
        }
    }
}