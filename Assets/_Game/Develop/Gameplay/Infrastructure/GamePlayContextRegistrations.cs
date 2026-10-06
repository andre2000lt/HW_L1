using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ControllersManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public static class GamePlayContextRegistrations
    {
        public static void Process(DIContainer container, GameplaySceneData gameplaySceneData)
        {
            container.RegisterAsSingleton(CreateGamePlayControllersFactory);
            container.RegisterAsSingleton(CreateGamePlayFactory);
            container.RegisterAsSingleton(CreateGameModeFactory);
            container.RegisterAsSingleton(CreateGamePlayCycleFactory);
        }

        private static GamePlayCycleFactory CreateGamePlayCycleFactory(DIContainer container)
        {
            return new GamePlayCycleFactory(container);
        }

        private static GameModeFactory CreateGameModeFactory(DIContainer container)
        {
            return new GameModeFactory(container);
        }

        private static GamePlayControllersFactory CreateGamePlayControllersFactory(DIContainer container)
        {
            return new GamePlayControllersFactory();
        }

        private static GamePlayFactory CreateGamePlayFactory(DIContainer container)
        {
            return new GamePlayFactory();
        }
    }
}