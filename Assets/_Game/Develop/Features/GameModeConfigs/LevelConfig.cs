using UnityEngine;

namespace _Game.Develop.Utils.ConfigServices.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public string ValidSymbols { get; private set; }
    }
}