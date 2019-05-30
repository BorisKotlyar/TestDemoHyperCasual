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


        #region Public Methods
        /// <summary>
        /// Play animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public override void Play(UnityAction onEndPlay = null)
        {
            base.Play(onEndPlay);

            Stop();

            var setted = false;
            foreach (var doAnimation in _content)
            {
                if (_played)
                    doAnimation.Stop();

                if (_overrideSettings)
                    doAnimation.SetSettings(_settings);

                doAnimation.Play(setted ? null : onEndPlay);
                setted = true;
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

            var setted = false;
            foreach (var doAnimation in _content)
            {
                if (_played)
                    doAnimation.Stop();

                if (_overrideSettings)
                    doAnimation.SetSettings(_settings);

                doAnimation.Rewind(setted ? null : onEndPlay);
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
    }
}
