using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ControllersManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public static class GamePlayContextRegistrations
    {
        public static void Process(DIContainer container, GameplaySceneData gameplaySceneData)
        {
            container.RegisterAsSingleton(CreateGamePlayControllersFactory);
        }

        private static GamePlayControllersFactory CreateGamePlayControllersFactory(DIContainer container)
        {
            return new GamePlayControllersFactory();
        }
    }
}