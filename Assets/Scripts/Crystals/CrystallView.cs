using TestDemo.Animations;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace TestDemo
{
    public class CrystallView : MonoBehaviour
    {
        [SerializeField] private Transform _cachedTransform;
        [SerializeField] private DOAnimation _animation;

        private Vector3 _position;

        [Inject] private readonly SignalBus _signalBus;

        [Inject]
        public Crystall Data
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
            // show animation
            _animation.Play();
        }

        public void Disappear(UnityAction onEndPlay)
        {
            // hide animation
            _animation.Rewind(onEndPlay);
        }

        public void OnTriggerEnter(Collider other)
        {
            var view = other.GetComponent<Player>();
            if (view != null)
            {
                _signalBus.Fire<CrystallCollectSignal>();
                Data.Die();
            }
        }
    }
}