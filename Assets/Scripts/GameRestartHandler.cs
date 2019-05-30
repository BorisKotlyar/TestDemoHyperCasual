using System;
using JacobGames.SuperInvoke;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestDemo
{
    public class GameRestartHandler : IInitializable, IDisposable, ITickable
    {
        private readonly SuperInvokeTag _siTag = SuperInvoke.CreateTag();

        private readonly SignalBus _signalBus;
        private bool _dead;
        private bool _activeTransition;

        public GameRestartHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerFallSignal>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerFallSignal>(OnPlayerDied);
        }

        public void Tick()
        {
            if (!_activeTransition && _dead && Input.anyKeyDown)
            {
                _activeTransition = true;

                SuperInvoke.Kill(_siTag);
                SuperInvoke.Run(()=>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                    // TODO:[BORIS] move delay time to settings?
                }, 0.5f, _siTag);
            }
        }

        private void OnPlayerDied()
        {
            _dead = true;
        }
    }
}