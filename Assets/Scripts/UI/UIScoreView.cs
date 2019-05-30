using TestDemo.Animations;
using TMPro;
using UnityEngine;
using Zenject;

namespace TestDemo.UI
{
    public class UIScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private DOAnimation _animation;

        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly GameSettings _gameSettings;

        // [TODO]:Move to game logic handler?
        private int _collectedCount = 0;

        protected void OnEnable()
        {
            _signalBus.Subscribe<CrystallCollectSignal>(OnCrystallCollect);
            _animation.Reset();
        }

        protected void OnDisable()
        {
            _signalBus.Unsubscribe<CrystallCollectSignal>(OnCrystallCollect);
        }

        private void OnCrystallCollect(CrystallCollectSignal obj)
        {
            if (_collectedCount == 0)
                _animation.Play();

            _collectedCount++;
            UpdateView();
        }

        private void UpdateView()
        {
            _scoreLabel.text = "Score: " + _collectedCount * _gameSettings.ScoreMultCounter;
        }
    }
}