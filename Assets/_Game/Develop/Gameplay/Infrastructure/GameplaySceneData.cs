using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.SceneManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GameplaySceneData : ISceneData
    {
        public GameplaySceneData(LevelConfig levelConfig)
        {
            LevelConfig = levelConfig;
        }

        public LevelConfig LevelConfig { get; }
    }
}