using TestDemo.Animations;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace TestDemo
{
    public class PlatformView : MonoBehaviour
    {
        [SerializeField] private Transform _cachedTransform;
        [SerializeField] private DOAnimation _animation;

        private Vector3 _position;

        [Inject]
        public Platform Data
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
    }
}