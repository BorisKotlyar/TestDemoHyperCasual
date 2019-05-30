using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace TestDemo.Animations
{
    public class DOAnimationTransform : DOAnimation
    {
        #region Editor Fields
        /// <summary>
        /// Animation content source
        /// </summary>
        [SerializeField] protected Transform _content;
        #endregion

        #region Public Methods
        /// <summary>
        /// Play animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public override void Play(UnityAction onEndPlay = null)
        {
            base.Play(onEndPlay);

            if (_played)
                _content.DOKill();

            _played = true;
        }

        /// <summary>
        /// Rewind animation, when ends hit event
        /// </summary>
        /// <param name="onEndPlay">on end play event</param>
        public override void Rewind(UnityAction onEndPlay = null)
        {
            base.Rewind(onEndPlay);

            if (_played)
                _content.DOKill();

            _played = true;
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public override void Stop()
        {
            base.Stop();

            if (!_played) return;

            _content.DOKill();

            _played = false;
        }
        #endregion
    }
}
