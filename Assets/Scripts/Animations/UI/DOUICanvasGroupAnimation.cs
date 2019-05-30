using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace TestDemo.Animations.UI
{
    public class DOUICanvasGroupAnimation : DOAnimation
    {
        #region Editor Fields
        /// <summary>
        /// Animation content (Canvas Group) source
        /// </summary>
        [SerializeField] protected CanvasGroup _canvasGroup;

        /// <summary>
        /// From\Start alfa
        /// </summary>
        [SerializeField] protected float _from;

        /// <summary>
        /// To\End alfa
        /// </summary>
        [SerializeField] protected float _to;
        #endregion

        #region Public Methods
        /// <summary>
        /// Play change animation from start to end alfa canvas group.
        /// On end play hit event.
        /// </summary>
        /// <param name="onEndPlay">On end play event.</param>
        public override void Play(UnityAction onEndPlay = null)
        {
            base.Play(onEndPlay);
            // DOTween animation

            if (_played)
                _canvasGroup.DOKill();

            _played = true;

            _canvasGroup.DOFade(_to, _settings.Duration)
                .SetDelay(_settings.Delay)
                .SetEase(_settings.Ease)
                .OnComplete(() =>
                {
                    onEndPlay?.Invoke();
                    _isAnimated = false;
                });
        }

        /// <summary>
        /// Rewind change animation from end to start alfa canvas group.
        /// On end play hit event.
        /// </summary>
        /// <param name="onEndPlay">On end play event.</param>
        public override void Rewind(UnityAction onEndPlay = null)
        {
            base.Rewind(onEndPlay);

            if (_played)
                _canvasGroup.DOKill();

            _played = true;

            // DOTween animation
            _canvasGroup.DOFade(_from, _settings.Duration)
                .SetDelay(_settings.Delay)
                .SetEase(_settings.Ease)
                .OnComplete(() =>
                {
                    onEndPlay?.Invoke();
                    _isAnimated = false;
                });
        }

        /// <summary>
        /// Reset animation and set start canvas froup alfa.
        /// </summary>
        public override void Reset()
        {
            base.Reset();

            Stop();

            _canvasGroup.alpha = _from;
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public override void Stop()
        {
            if (!_played) return;

            _canvasGroup.DOKill();

            base.Stop();

            _played = false;
        }
        #endregion
    }
}
