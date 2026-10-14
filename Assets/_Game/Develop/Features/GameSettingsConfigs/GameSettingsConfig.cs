using _Game.Develop.Meta.Features;
using UnityEngine;

namespace _Game.Develop.Features.GameSettingsConfigs
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configs/GameSettingsConfig")]
    public class GameSettingsConfig : ScriptableObject
    {
        [field: SerializeField]
        public int VictoryPrize { get; private set; }
        [field: SerializeField]
        public int DefeatPenalty { get; private set; }
        [field: SerializeField]
        public int StartBalance { get; private set; }
        [field: SerializeField]
        public int ResetStatsCost { get; private set; }

        public CurrencyType SelectedCurrencyType => CurrencyType.Gold;
    }
}