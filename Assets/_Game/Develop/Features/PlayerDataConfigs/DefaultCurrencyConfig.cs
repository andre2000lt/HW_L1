using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Develop.Meta.Features;
using UnityEngine;

namespace _Game.Develop.Gameplay
{
    [CreateAssetMenu(fileName = "DefaultCurrencyConfig", menuName = "Configs/PlayerData")]
    public class DefaultCurrencyConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencySettings> _currencies = new();

        public int GetAmountFor(CurrencyType currencyType)
        {
            CurrencySettings currency = _currencies.FirstOrDefault(x => x.CurrencyType == currencyType);

            if (currency == null) return 0;

            return currency.Amount;
        }

        [Serializable]
        private class CurrencySettings
        {
            [field: SerializeField]
            public CurrencyType CurrencyType { get; private set; }
            [field: SerializeField]
            public int Amount { get; private set; }
        }
    }
}