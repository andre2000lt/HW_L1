using System.Collections;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GamePlayCycle
    {
        private const KeyCode StartKey = KeyCode.F;
        private const KeyCode RestartKey = KeyCode.Space;

        private readonly LevelConfig _levelConfig;
        private readonly DIContainer _container;
        private GameMode _gameMode;

        private ICoroutinesRunner _coroutinesRunner;

        public GamePlayCycle(LevelConfig levelConfig, DIContainer container)
        {
            _levelConfig = levelConfig;
            _container = container;

            _coroutinesRunner = _container.Resolve<ICoroutinesRunner>();
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        public IEnumerator Launch()
        {
            _gameMode = new GameMode(_container, _levelConfig);
            _gameMode.GameWon += OnGameWon;
            _gameMode.GameLost += OnGameLost;

            Debug.Log($"Press {StartKey} to Start Game");
            yield return new WaitUntil(() => Input.GetKeyDown(StartKey));

            _gameMode.Start();
        }

        private void OnGameLost()
        {
            _coroutinesRunner.Perform(GameLostProcess());
        }

        private IEnumerator GameLostProcess()
        {
            _gameMode.GameLost -= OnGameLost;

            Debug.Log("Game Lost");
            Debug.Log($"Press {RestartKey}");
            yield return new WaitUntil(() => Input.GetKeyDown(RestartKey));

            yield return Launch();
        }

        private void OnGameWon()
        {
            _coroutinesRunner.Perform(GameWonProcess());
        }

        private IEnumerator GameWonProcess()
        {
            _gameMode.GameWon -= OnGameWon;

            Debug.Log("Game Won");
            Debug.Log($"Press {RestartKey}");
            yield return new WaitUntil(() => Input.GetKeyDown(RestartKey));

            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            yield return sceneSwitcher.ProcessSwitchTo(SceneName.MainMenu);
        }
    }
}