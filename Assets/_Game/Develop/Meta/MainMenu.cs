using _Game.Develop.Features.GameSettingsConfigs;
using _Game.Develop.Gameplay.Infrastructure;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Meta
{
    public class MainMenu
    {
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesRunner _coroutinesRunner;

        private readonly StatsService _statsService;
        private readonly WalletService _walletService;
        private readonly MarketService _marketService;

        private readonly LevelConfigs _levelConfigs;
        private readonly GameSettingsConfig _gameSettingsConfig;

        public MainMenu(
            SceneSwitcherService sceneSwitcher,
            ConfigProviderService configProvider,
            ICoroutinesRunner coroutinesRunner,
            StatsService statsService,
            WalletService walletService,
            MarketService marketService
        )
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesRunner = coroutinesRunner;

            _statsService = statsService;
            _walletService = walletService;
            _marketService = marketService;

            _levelConfigs = configProvider.GetConfig<LevelConfigs>();
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
            _coroutinesRunner.Perform(_marketService.ResetStatsProcess());
        }
    }
}