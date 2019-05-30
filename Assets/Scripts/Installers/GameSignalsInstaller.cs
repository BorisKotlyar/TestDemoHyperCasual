using Zenject;

namespace TestDemo
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlatformCollisionSignal>();
            Container.DeclareSignal<PlayerFallSignal>();
            Container.DeclareSignal<PlatformAppearSignal>().OptionalSubscriber();

            Container.DeclareSignal<CrystallAppearSignal>().OptionalSubscriber();
            Container.DeclareSignal<CrystallCollectSignal>().OptionalSubscriber();
        }
    }
}