using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.ControllersManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GameModeFactory
    {
        private readonly DIContainer _container;

        public GameModeFactory(DIContainer container)
        {
            _container = container;
        }

        public GameMode CreateGameMode(LevelConfig levelConfig)
        {
            GamePlayFactory gamePlayFactory = _container.Resolve<GamePlayFactory>();
            GamePlayControllersFactory gamePlayControllersFactory = _container.Resolve<GamePlayControllersFactory>();

            return new GameMode(gamePlayFactory, gamePlayControllersFactory, levelConfig);
        }
    }
}