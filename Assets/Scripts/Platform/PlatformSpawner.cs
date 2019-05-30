using System;
using System.Collections.Generic;
using JacobGames.SuperInvoke;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace TestDemo
{
    public class PlatformSpawner : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly Platform.Factory _factory;
        private readonly SpawnSettings _settings;

        private readonly SuperInvokeTag _siTag = SuperInvoke.CreateTag();

        private Vector3 _lastPlatformPosition;
        private Vector3 _lastSpawnPoint;

        private int _spawnedCount;
        private List<Platform> _spawnedPlatforms = new List<Platform>();

        public PlatformSpawner(SpawnSettings settings, SignalBus signalBus, Platform.Factory factory)
        {
            _signalBus = signalBus;
            _factory = factory;
            _settings = settings;
        }

        #region IInitializable implementation
        public void Initialize()
        {
            _signalBus.Subscribe<PlatformCollisionSignal>(OnPlatformHide);

            var initPositions = new List<SpawnInitData>();
            if (_settings.InitialSpawn != null && _settings.InitialSpawn.Length > 0)
            {
                for (var i = 0; i < _settings.InitialSpawn.Length; i++)
                {
                    var spawnData = _settings.InitialSpawn[i];
                    SpawnPlatform(spawnData.Position);
                    
                    if (spawnData.IsStartPosition)
                        initPositions.Add(spawnData);
                }

                var rndValue = Random.Range(0, initPositions.Count);
                _lastSpawnPoint = initPositions[rndValue].Position;

                CheckSpawn();
            }
            else
            {
                Debug.LogError("[PlatformSpawner::Initialize] No init data for spawn!");
            }
        }
        #endregion

        private Vector3 SpawnPlatform(Vector3 placePosition, bool withRandom = false)
        {
            var platform = _factory.Create();

            if (!withRandom)
            {
                platform.Position = placePosition;
            }
            else
            {
                var direction = Random.Range(0, 2);
                platform.Position = direction == 0 ? 
                    new Vector3(placePosition.x + 1, placePosition.y, placePosition.z) : 
                    new Vector3(placePosition.x, placePosition.y, placePosition.z + 1);
            }

            _spawnedPlatforms.Add(platform);

            return platform.Position;
        }

        private void OnPlatformHide(PlatformCollisionSignal platform)
        {
            var pl = _spawnedPlatforms[0];
            _spawnedPlatforms.RemoveAt(0);
            pl.Die();

            _spawnedCount--;
            CheckSpawn();
        }

        private void CheckSpawn()
        {
            SuperInvoke.Kill(_siTag);

            if (_spawnedCount < _settings.MinCountOnScreen)
            {
                _spawnedCount++;

                var pos = SpawnPlatform(_lastSpawnPoint, true);

                _signalBus.Fire(new PlatformAppearSignal() { Position = pos });
                _lastSpawnPoint = pos;

                SuperInvoke.Run(CheckSpawn, _settings.DelayTime, _siTag);
            }
        }

        [Serializable]
        public class SpawnSettings
        {
            public SpawnInitData[] InitialSpawn;
            public int MinCountOnScreen = 5;
            public float DelayTime = 0.3f;
        }

        [Serializable]
        public class SpawnInitData
        {
            public Vector3 Position;
            public bool IsStartPosition;
        }
    }
}