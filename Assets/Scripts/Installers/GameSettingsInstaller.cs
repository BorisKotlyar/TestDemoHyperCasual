using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    [CreateAssetMenu(menuName = "TestDemo/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("Platforms")]
        public PlatformSpawner.SpawnSettings PlatformSpawner;
        public PrefabSettings PrefabSettings;

        [Header("Player")]
        public PlayerSettings PlayerSettings;

        [Header("Game")]
        public GameSettings GameSettings;

        [Header("Crystall")]
        public CrystallSpawner.SpawnSettings CrystallSpawnSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(PlatformSpawner).IfNotBound();
            Container.BindInstance(PrefabSettings).IfNotBound();
            Container.BindInstance(GameSettings).IfNotBound();
            Container.BindInstance(CrystallSpawnSettings).IfNotBound();

            Container.BindInstance(PlayerSettings.PlayerMoveHandler).IfNotBound();
            Container.BindInstance(PlayerSettings.PlayerFallHandler).IfNotBound();
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveHandler.Settings PlayerMoveHandler;
        public PlayerFallHandler.Settings PlayerFallHandler;
    }

    [Serializable]
    public class GameSettings
    {
        public int ScoreMultCounter;
    }
}
