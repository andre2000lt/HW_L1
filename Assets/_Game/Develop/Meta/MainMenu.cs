using System.Collections;
using _Game.Develop.Features.GameSettingsConfigs;
using _Game.Develop.Gameplay.Infrastructure;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Meta
{
    public class MainMenu
    {
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesRunner _coroutinesRunner;
        private readonly WalletService _walletService;
        private WalletDataProvider _walletDataProvider;
        private readonly StatsService _statsService;
        private StatsDataProvider _statsDataProvider;

        private readonly LevelConfigs _levelConfigs;
        private readonly GameSettingsConfig _gameSettingsConfig;

        public MainMenu(
            SceneSwitcherService sceneSwitcher,
            ConfigProviderService configProvider,
            ICoroutinesRunner coroutinesRunner,
            WalletService walletService,
            WalletDataProvider walletDataProvider,
            StatsService statsService,
            StatsDataProvider statsDataProvider
        )
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesRunner = coroutinesRunner;
            _walletService = walletService;
            _walletDataProvider = walletDataProvider;
            _statsService = statsService;
            _statsDataProvider = statsDataProvider;

            _levelConfigs = configProvider.GetConfig<LevelConfigs>();
            _gameSettingsConfig = configProvider.GetConfig<GameSettingsConfig>();
        }

        public void StartGame(KeyCode keyCode)
        {
            int id = KeyCodeConverter.ToInt(keyCode);
            LevelConfig levelConfig = _levelConfigs.GetConfigBy(id);

            _coroutinesRunner.Perform(_sceneSwitcher.ProcessSwitchTo(SceneName.Gameplay, new GameplaySceneData(levelConfig)));
        }

        public void ShowStats()
        {
            Debug.Log("Victory Count : " + _statsService.VictoryCount);
            Debug.Log("Defeat Count : " + _statsService.DefeatCount);
            Debug.Log("Gold Amount : " + _walletService.GetCurrencyAmount(CurrencyType.Gold).Value);
        }

        public void ResetStats()
        {
            _coroutinesRunner.Perform(ResetStatsProcess());
        }

        private IEnumerator ResetStatsProcess()
        {
            int resetStatsCost = _gameSettingsConfig.ResetStatsCost;
            CurrencyType currencyType = _gameSettingsConfig.SelectedCurrencyType;

            if (_walletService.IsEnough(currencyType, resetStatsCost))
            {
                Debug.Log("Statistics reset ! ");

                _statsDataProvider.Reset();
                yield return _statsDataProvider.Save();

                _walletService.Spend(CurrencyType.Gold, resetStatsCost);
                yield return _walletDataProvider.Save();
            }
            else
            {
                Debug.Log("Not enough amount of: " + currencyType);
            }
        }
    }
}