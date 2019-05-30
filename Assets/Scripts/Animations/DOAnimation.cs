using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace TestDemo.Animations
{
    public class DOAnimation : MonoBehaviour
    {
        #region Editor Fields
        /// <summary>
        /// Serialized settings
        /// </summary>
        [SerializeField] protected DOAnimationSettings _settings;

        /// <summary>
        /// Call "Reset" method on awake?
        /// </summary>
        [SerializeField] protected bool _resetOnAwake;

        #endregion
        protected Tween _tween;

        /// <summary>
        /// Current animation state
        /// </summary>
        protected bool _played;

        public bool IsPlayed { get { return _played; } }

        protected bool _isAnimated;
        public bool IsAnimated { get { return _isAnimated; } }


        public float Duration
        {
            get { return _settings.Duration; }
        }

        #region Root Methods

        protected virtual void Awake()
        {
            if (_resetOnAwake)
            {
                Reset();
            }
        }

        #endregion


        #region Public Methods
        /// <summary>
        /// Play animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public virtual void Play(UnityAction onEndPlay = null)
        {

            if (_played && _tween != null)
                _tween.Kill();

            //_played = true;

            _isAnimated = true;
        }

        /// <summary>
        /// Rewind animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public virtual void Rewind(UnityAction onEndPlay = null)
        {
            if (_played && _tween != null)
                _tween.Kill();

            //_played = true;

            _isAnimated = true;
        }

        public virtual void Play()
        {
            Stop();
            Play(null);
        }

        public virtual void Rewind()
        {
            Stop();
            Rewind(null);
        }

        /// <summary>
        /// Reset animation and params
        /// </summary>
        public virtual void Reset()
        {
            _isAnimated = false;
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public virtual void Stop()
        {
            if (!_played) return;

            _tween.Kill();

            _played = false;
            _isAnimated = false;
        }

        /// <summary>
        /// Animated reset. Run with custom animation settings.
        /// </summary>
        /// <param name="settings">Custom animation settings</param>
        public virtual void FastResetTo(DOAnimationSettings settings)
        {

        }

        public DOAnimationSettings GetSettings()
        {
            return _settings;
        }

        public void SetSettings(DOAnimationSettings settings)
        {
            _settings = settings;
        }
        #endregion
    }
}
