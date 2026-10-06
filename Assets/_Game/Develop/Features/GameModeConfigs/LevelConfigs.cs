using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Develop.Utils.ConfigServices.Configs
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/LevelConfigs")]
    public class LevelConfigs : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levelConfigs;

        public LevelConfig GetConfigBy(int configId)
        {
            int index = configId - 1;

            if (index >= 0 && index < _levelConfigs.Count)
            {
                return _levelConfigs[index];
            }

            throw new IndexOutOfRangeException($"Config with id {configId} not found");
        }
    }
}