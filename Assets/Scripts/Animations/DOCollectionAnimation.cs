using UnityEngine;
using UnityEngine.Events;

namespace TestDemo.Animations
{
    public class DOCollectionAnimation : DOAnimation
    {
        #region Editor Fields
        /// <summary>
        /// Animation content source
        /// </summary>
        [SerializeField] protected DOAnimation[] _content;

        /// <summary>
        /// Need override all settings for content animation?
        /// </summary>
        [SerializeField] protected bool _overrideSettings;
        #endregion

        private int _compliteAnimationCounter;
        private UnityAction _cachedAcion;

        #region Public Methods
        /// <summary>
        /// Play animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public override void Play(UnityAction onEndPlay = null)
        {
            base.Play(onEndPlay);

            Stop();
            _cachedAcion = onEndPlay;

            _compliteAnimationCounter = 0;

            foreach (var doAnimation in _content)
            {
                if (_played)
                    doAnimation.Stop();

                if (_overrideSettings)
                    doAnimation.SetSettings(_settings);

                doAnimation.Play(OnEndPlay);
            }
            
            _played = true;
        }

        /// <summary>
        /// Rewind animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public override void Rewind(UnityAction onEndPlay = null)
        {
            base.Rewind(onEndPlay);

            Stop();
            _cachedAcion = onEndPlay;

            _compliteAnimationCounter = 0;

            foreach (var doAnimation in _content)
            {
                if (_played)
                    doAnimation.Stop();

                if (_overrideSettings)
                    doAnimation.SetSettings(_settings);

                doAnimation.Rewind(OnEndPlay);
            }

            _played = true;
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public override void Stop()
        {
            base.Stop();

            if (!_played) return;

            foreach (var doAnimation in _content)
            {
                doAnimation.Stop();
            }

            _played = false;
        }
        #endregion

        private void OnEndPlay()
        {
            if (_compliteAnimationCounter < 0)
                return;

            _compliteAnimationCounter++;
            CheckComplition();
        }

        private void CheckComplition()
        {
            if (_compliteAnimationCounter == _content.Length)
            {
                _compliteAnimationCounter = -1;
                _cachedAcion?.Invoke();
            }
        }
    }
}
