using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Develop.IReadOnlyReactVariable;
using _Game.Develop.Utils.DataManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;

namespace _Game.Develop.Meta.Features
{
    public class WalletService : IDataReader<WalletData>, IDataWriter<WalletData>
    {
        private readonly Dictionary<CurrencyType, ReactVariable<int>> _currencies;

        public WalletService(
            Dictionary<CurrencyType, ReactVariable<int>> currencies,
            WalletDataProvider dataProvider)
        {
            _currencies = new Dictionary<CurrencyType, ReactVariable<int>>(currencies);

            dataProvider.RegisterReader(this);
            dataProvider.RegisterWriter(this);
        }

        public List<CurrencyType> CurrencyTypes => _currencies.Keys.ToList();

        public IReadOnlyReactVariable<int> GetCurrencyAmount(CurrencyType currencyType)
        {
            ReactVariable<int> amount = _currencies[currencyType];
            return amount;
        }

        public bool IsEnough(CurrencyType currencyType, int amount)
        {
            return _currencies[currencyType].Value >= amount;
        }

        public void AddCurrency(CurrencyType currencyType, int amount)
        {
            if (amount < 0)
                throw new Exception("Amount can't be negative");

            _currencies[currencyType].Value += amount;
        }

        public void Spend(CurrencyType currencyType, int amount)
        {
            if (amount < 0)
                throw new Exception("Amount can't be negative");

            if (IsEnough(currencyType, amount) == false)
                return;

            _currencies[currencyType].Value -= amount;
        }

        public void ReadFrom(WalletData data)
        {
            foreach (KeyValuePair<CurrencyType, int> currency in data.Currencies)
            {
                if (_currencies.ContainsKey(currency.Key))
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies[currency.Key] = new ReactVariable<int>(currency.Value);
            }
        }

        public void WriteTo(WalletData data)
        {
            foreach (KeyValuePair<CurrencyType, ReactVariable<int>> currency in _currencies)
            {
                data.Currencies[currency.Key] = currency.Value.Value;
            }
        }
    }
}