using UnityEngine;

namespace _Game.Develop.Utils.ConfigServices.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameModeType GameMode { get; private set; }
    }
}