using _Game.Develop.Gameplay;

namespace _Game.Develop.Utils.ControllersManagement
{
    public class GamePlayControllersFactory
    {
        public GamePlayController CreateGamePlayController(GamePlay gamePlay)
        {
            return new GamePlayController(gamePlay);
        }
    }
}