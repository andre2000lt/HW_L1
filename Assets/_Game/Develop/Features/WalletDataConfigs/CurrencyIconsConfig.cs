using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Develop.Meta.Features;
using UnityEngine;

namespace _Game.Develop.Gameplay
{
    [CreateAssetMenu(fileName = "CurrencyIconsConfig", menuName = "Configs/CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencySettings> _currencies = new();

        public Sprite GetSpriteFor(CurrencyType currencyType)
        {
            CurrencySettings currency = _currencies.FirstOrDefault(x => x.CurrencyType == currencyType);

            if (currency == null)
            {
                throw new Exception($"Sprite for currency {currencyType} not found");
            }

            return currency.Sprite;
        }

        [Serializable]
        private class CurrencySettings
        {
            [field: SerializeField]
            public CurrencyType CurrencyType { get; private set; }
            [field: SerializeField]
            public Sprite Sprite { get; private set; }
        }
    }
}