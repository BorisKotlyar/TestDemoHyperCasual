using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace TestDemo.Animations
{
    public class DOLocalScaleAnimation : DOAnimationTransform
    {
        #region Editor Fields
        /// <summary>
        /// From\Start scale
        /// </summary>
        [SerializeField] private Vector3 _from;

        /// <summary>
        /// To\End scale
        /// </summary>
        [SerializeField] private Vector3 _to;
        #endregion



        #region Public Methods
        /// <summary>
        /// Play move animation from start to end scale.
        /// On end play hit event.
        /// </summary>
        /// <param name="onEndPlay">On end play event.</param>
        public override void Play(UnityAction onEndPlay = null)
        {
            base.Play(onEndPlay);

            // DOTween animation
            _content.DOScale(_to, _settings.Duration)
                .SetDelay(_settings.Delay)
                .SetEase(_settings.Ease)
                .OnComplete(() =>
                {
                    onEndPlay?.Invoke();
                });

        }

        /// <summary>
        /// Play move animation from end to start scale.
        /// On end play hit event.
        /// </summary>
        /// <param name="onEndPlay">On end play event.</param>
        public override void Rewind(UnityAction onEndPlay = null)
        {
            base.Rewind(onEndPlay);

            // DOTween animation
            _content.DOScale(_from, _settings.Duration)
                .SetDelay(_settings.Delay)
                .SetEase(_settings.Ease)
                .OnComplete(() =>
                {
                    onEndPlay?.Invoke();
                });
        }

        /// <summary>
        /// Reset animation and set start scale.
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            _content.localScale = _from;
        }
        #endregion
    }
}
