using System.Collections;
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
        private GameMode _gameMode;

        private readonly GameModeFactory _gameModeFactory;
        private readonly ICoroutinesRunner _coroutinesRunner;
        private readonly SceneSwitcherService _sceneSwitcher;

        public GamePlayCycle(
            LevelConfig levelConfig,
            GameModeFactory gameModeFactory,
            ICoroutinesRunner coroutinesRunner,
            SceneSwitcherService sceneSwitcher
        )
        {
            _levelConfig = levelConfig;
            _gameModeFactory = gameModeFactory;
            _coroutinesRunner = coroutinesRunner;
            _sceneSwitcher = sceneSwitcher;
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        public IEnumerator Launch()
        {
            _gameMode = _gameModeFactory.CreateGameMode(_levelConfig);
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

            yield return _sceneSwitcher.ProcessSwitchTo(SceneName.MainMenu);
        }
    }
}