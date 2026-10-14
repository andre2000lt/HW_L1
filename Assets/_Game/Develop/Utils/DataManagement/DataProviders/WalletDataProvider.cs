using System.Collections.Generic;
using _Game.Develop.Features.GameSettingsConfigs;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils.ConfigServices;

namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public class WalletDataProvider : DataProvider<WalletData>
    {
        private readonly ConfigProviderService _configProvider;

        public WalletDataProvider(
            ISaveLoadService saveLoadService,
            ConfigProviderService configProvider) : base(saveLoadService)
        {
            _configProvider = configProvider;
        }

        protected override WalletData GetOriginData()
        {
            WalletData data = new()
            {
                Currencies = InitCurrencies()
            };

            return data;
        }

        private Dictionary<CurrencyType, int> InitCurrencies()
        {
            Dictionary<CurrencyType, int> currencies = new();

            GameSettingsConfig gameSettingsConfig = _configProvider.GetConfig<GameSettingsConfig>();

            CurrencyType currencyType = gameSettingsConfig.SelectedCurrencyType;
            int amount = gameSettingsConfig.StartBalance;
            currencies.Add(currencyType, amount);

            return currencies;
        }
    }
}