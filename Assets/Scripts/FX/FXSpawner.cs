using Zenject;

namespace TestDemo
{
    public class FXSpawner : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly FX.Factory _factory;

        public FXSpawner(SignalBus signalBus, FX.Factory factory)
        {
            _signalBus = signalBus;
            _factory = factory;
        }

        #region IInitializable implementation
        public void Initialize()
        {
            _signalBus.Subscribe<CrystallAppearSignal>(OnAppearCrystall);
        }
        #endregion

        private void OnAppearCrystall(CrystallAppearSignal obj)
        {
            var fx = _factory.Create();
            fx.Position = obj.Position;
        }
    }
}