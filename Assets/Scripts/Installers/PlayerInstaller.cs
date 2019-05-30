using System;
using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().AsSingle().WithArguments(_settings.Rigidbody);

            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();

            Container.Bind<PlayerInputState>().AsSingle();

            Container.BindInterfacesTo<PlayerFallHandler>().AsSingle();
        }


        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
        }
    }
}