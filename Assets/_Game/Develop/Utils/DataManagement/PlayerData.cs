using System.Collections.Generic;
using _Game.Develop.Meta.Features;

namespace _Game.Develop.Utils.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyType, int> Currencies = new();
    }
}