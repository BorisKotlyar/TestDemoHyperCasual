using System;
using Zenject;
using Random = UnityEngine.Random;

namespace TestDemo
{
    public class CrystallSpawner : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly Crystall.Factory _factory;
        private readonly SpawnSettings _settings;

        public CrystallSpawner(SpawnSettings settings, SignalBus signalBus, Crystall.Factory factory)
        {
            _signalBus = signalBus;
            _factory = factory;
            _settings = settings;
        }

        #region IInitializable implementation
        public void Initialize()
        {
            _signalBus.Subscribe<PlatformAppearSignal>(OnAppearPlatform);
        }
        #endregion

        private void OnAppearPlatform(PlatformAppearSignal obj)
        {
            var rnd = Random.Range(0, 100);
            if (rnd > _settings.Chance)
            {
                var crystall = _factory.Create();
                crystall.Position = obj.Position;

                _signalBus.Fire(new CrystallAppearSignal() {Position = crystall.Position});
            }
        }

        [Serializable]
        public class SpawnSettings
        {
            public int Chance;
        }
    }
}