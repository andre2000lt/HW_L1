using System.Collections;
using _Game.Develop.Features.GameSettingsConfigs;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.DataManagement.DataProviders;
using UnityEngine;

namespace _Game.Develop.Meta.Features
{
    public class MarketService
    {
        private readonly StatsService _statsService;
        private readonly StatsDataProvider _statsDataProvider;

        private readonly WalletService _walletService;
        private readonly WalletDataProvider _walletDataProvider;

        private readonly ConfigProviderService _configProvider;

        private readonly GameSettingsConfig _gameSettingsConfig;

        public MarketService(
            StatsService statsService,
            StatsDataProvider statsDataProvider,
            WalletService walletService,
            WalletDataProvider walletDataProvider,
            ConfigProviderService configProvider
        )
        {
            _statsService = statsService;
            _statsDataProvider = statsDataProvider;
            _walletService = walletService;
            _walletDataProvider = walletDataProvider;

            _gameSettingsConfig = configProvider.GetConfig<GameSettingsConfig>();
        }

        public IEnumerator ResetStatsProcess()
        {
            int resetStatsCost = _gameSettingsConfig.ResetStatsCost;
            CurrencyType currencyType = _gameSettingsConfig.SelectedCurrencyType;

            if (_walletService.IsEnough(currencyType, resetStatsCost))
            {
                Debug.Log("Statistics reset ! ");

                _statsService.Reset();
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