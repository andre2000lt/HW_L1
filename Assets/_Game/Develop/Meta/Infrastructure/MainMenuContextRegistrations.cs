using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ControllersManagement;

namespace _Game.Develop.Meta.Infrastructure
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingleton(CreateMainMenuControllersFactory);
        }

        private static MainMenuControllersFactory CreateMainMenuControllersFactory(DIContainer container)
        {
            return new MainMenuControllersFactory();
        }
    }
}