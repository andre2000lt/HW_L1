using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ControllersManagement;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;

namespace _Game.Develop.Meta.Infrastructure
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingleton(CreateMainMenuControllersFactory);
            container.RegisterAsSingleton(CreateMainMenu);
        }

        private static MainMenu CreateMainMenu(DIContainer container)
        {
            SceneSwitcherService sceneSwitcher = container.Resolve<SceneSwitcherService>();
            ConfigProviderService configsProvider = container.Resolve<ConfigProviderService>();
            ICoroutinesRunner coroutinesRunner = container.Resolve<ICoroutinesRunner>();

            return new MainMenu(sceneSwitcher, configsProvider, coroutinesRunner);
        }

        private static MainMenuControllersFactory CreateMainMenuControllersFactory(DIContainer container)
        {
            return new MainMenuControllersFactory();
        }
    }
}