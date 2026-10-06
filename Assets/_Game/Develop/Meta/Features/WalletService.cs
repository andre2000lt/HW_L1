using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Develop.IReadOnlyReactVariable;

namespace _Game.Develop.Meta.Features
{
    public class WalletService
    {
        private Dictionary<CurrencyType, ReactVariable<int>> _currencies = new();

        public WalletService(Dictionary<CurrencyType, ReactVariable<int>> currencies)
        {
            _currencies = new Dictionary<CurrencyType, ReactVariable<int>>(currencies);
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
    }
}