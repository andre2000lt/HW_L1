using System.Collections;
using _Game.Develop.Features.GameSettingsConfigs;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;
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

        private readonly WalletService _walletService;
        private WalletDataProvider _walletDataProvider;

        private readonly StatsService _statsService;
        private readonly StatsDataProvider _statsDataProvider;

        private GameSettingsConfig _gameSettingsConfig;

        public GamePlayCycle(
            LevelConfig levelConfig,
            GameModeFactory gameModeFactory,
            ICoroutinesRunner coroutinesRunner,
            SceneSwitcherService sceneSwitcher,
            ConfigProviderService configProviderService,
            StatsService statsService,
            StatsDataProvider statsDataProvider,
            WalletService walletService,
            WalletDataProvider walletDataProvider
        )
        {
            _levelConfig = levelConfig;
            _gameModeFactory = gameModeFactory;
            _coroutinesRunner = coroutinesRunner;
            _sceneSwitcher = sceneSwitcher;
            _statsService = statsService;
            _statsDataProvider = statsDataProvider;
            _walletService = walletService;
            _walletDataProvider = walletDataProvider;

            _gameSettingsConfig = configProviderService.GetConfig<GameSettingsConfig>();
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

            _statsService.IncreaseDefeatCount();
            yield return _statsDataProvider.Save();

            int amount = _gameSettingsConfig.DefeatPenalty;
            CurrencyType currencyType = _gameSettingsConfig.SelectedCurrencyType;
            _walletService.Spend(currencyType, amount);
            yield return _walletDataProvider.Save();

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

            _statsService.IncreaseVictoryCount();
            yield return _statsDataProvider.Save();

            int amount = _gameSettingsConfig.VictoryPrize;
            CurrencyType currencyType = _gameSettingsConfig.SelectedCurrencyType;
            _walletService.AddCurrency(currencyType, amount);
            yield return _walletDataProvider.Save();

            Debug.Log("Game Won");
            Debug.Log($"Press {RestartKey}");
            yield return new WaitUntil(() => Input.GetKeyDown(RestartKey));

            yield return _sceneSwitcher.ProcessSwitchTo(SceneName.MainMenu);
        }
    }
}