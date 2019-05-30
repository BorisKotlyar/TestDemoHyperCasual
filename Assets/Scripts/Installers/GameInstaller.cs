using Zenject;

namespace TestDemo
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private readonly PrefabSettings _prefabSettings;

        public override void InstallBindings()
        {
            // Bind spawner
            Container.BindInterfacesAndSelfTo<PlatformSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<CrystallSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<FXSpawner>().AsSingle();

            // Bind platform pool
            Container.BindFactory<Platform, Platform.Factory>()
                .FromPoolableMemoryPool<Platform, PlatformPool>(pool => pool
                    .WithInitialSize(_prefabSettings.Platform.InitialCount)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<PlatformInstaller>(_prefabSettings.Platform.GO)
                    .UnderTransformGroup(_prefabSettings.Platform.Group));

            // Bind crystall pool
            Container.BindFactory<Crystall, Crystall.Factory>()
                .FromPoolableMemoryPool<Crystall, CrystallPool>(pool => pool
                    .WithInitialSize(_prefabSettings.Crystall.InitialCount)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<CrystallInstaller>(_prefabSettings.Crystall.GO)
                    .UnderTransformGroup(_prefabSettings.Crystall.Group));

            // Bind fx pool
            Container.BindFactory<FX, FX.Factory>()
                .FromPoolableMemoryPool<FX, FXPool>(pool => pool
                    .WithInitialSize(_prefabSettings.FX.InitialCount)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<FXInstaller>(_prefabSettings.FX.GO)
                    .UnderTransformGroup(_prefabSettings.FX.Group));


            Container.BindInterfacesTo<GameRestartHandler>().AsSingle();

            // Install signals
            GameSignalsInstaller.Install(Container);
        }

        private class PlatformPool : MonoPoolableMemoryPool<IMemoryPool, Platform>
        {
        }

        private class CrystallPool : MonoPoolableMemoryPool<IMemoryPool, Crystall>
        {
        }

        private class FXPool : MonoPoolableMemoryPool<IMemoryPool, FX>
        {
        }
    }
}

