using UnityEngine;

namespace _Game.Develop.Utils.ConfigServices.Configs
{
    [CreateAssetMenu(fileName = "TestConfig", menuName = "Configs/TestConfig")]
    public class TestConfig : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
    }
}