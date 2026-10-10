using System;
using System.Collections.Generic;
using _Game.Develop.Gameplay;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils.ConfigServices;

namespace _Game.Develop.Utils.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private ConfigProviderService _configProvider;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigProviderService configProvider) : base(saveLoadService)
        {
            _configProvider = configProvider;
        }

        protected override PlayerData GetOriginData()
        {
            PlayerData data = new()
            {
                Currencies = InitCurrencies()
            };

            return data;
        }

        private Dictionary<CurrencyType, int> InitCurrencies()
        {
            Dictionary<CurrencyType, int> currencies = new();
            DefaultCurrencyConfig defaultCurrencies = _configProvider.GetConfig<DefaultCurrencyConfig>();

            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                int amount = defaultCurrencies.GetAmountFor(currencyType);
                currencies.Add(currencyType, amount);
            }

            return currencies;
        }
    }
}