using System;
using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GamePlayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplaySceneData _gameplaySceneData;

        private GamePlayCycle _gamePlayCycle;

        private void Update()
        {
            _gamePlayCycle?.Update(Time.deltaTime);
        }

        public override void ProcessRegistrations(DIContainer container, ISceneData sceneData)
        {
            _container = container;

            if (sceneData is not GameplaySceneData gameplaySceneData)
            {
                throw new Exception($"Gameplay Bootstrap Initialize: sceneData is not {typeof(GameplaySceneData)}");
            }

            _gameplaySceneData = gameplaySceneData;
            GamePlayContextRegistrations.Process(container, _gameplaySceneData);
        }

        public override IEnumerator Initialize()
        {
            LevelConfig LevelConfig = _gameplaySceneData.LevelConfig;

            GamePlayCycleFactory gamePlayCycleFactory = _container.Resolve<GamePlayCycleFactory>();
            _gamePlayCycle = gamePlayCycleFactory.CreateGamePlayCycle(LevelConfig);

            yield return new WaitForSeconds(0.5f);

            Debug.Log("Gameplay Bootstrap Initialized");
        }

        public override void Run()
        {
            Debug.Log("Gameplay Bootstrap Run");
            StartCoroutine(_gamePlayCycle.Launch());
        }
    }
}