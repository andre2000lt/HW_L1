using _Game.Develop.Utils.ConfigServices.Configs;

namespace _Game.Develop.Gameplay
{
    public class GamePlayFactory
    {
        public GamePlay CreateGamePlay(LevelConfig levelConfig)
        {
            return new GamePlay(levelConfig);
        }
    }
}