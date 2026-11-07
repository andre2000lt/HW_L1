#region

using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Meta.Features;
using _Game.Develop.UI;
using _Game.Develop.UI.Core;
using _Game.Develop.UI.MainMenu;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ControllersManagement;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

#endregion

namespace _Game.Develop.Meta.Infrastructure
{
    public static class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingleton(CreateMainMenuControllersFactory);
            container.RegisterAsSingleton(CreateMainMenu);
            container.RegisterAsSingleton(CreateMainMenuUIRoot).NonLazy();
            container.RegisterAsSingleton(CreateMainMenuPresentersFactory);
            container.RegisterAsSingleton(CreateMainMenuScreenPresenter).NonLazy();
            container.RegisterAsSingleton(CreateMainMenuPopupService);
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer container)
        {
            MainMenuUIRoot mainMenuUIRoot = container.Resolve<MainMenuUIRoot>();
            GlobalPresentersFactory globalPresentersFactory = container.Resolve<GlobalPresentersFactory>();
            ViewsFactory viewsFactory = container.Resolve<ViewsFactory>();

            return new MainMenuPopupService(viewsFactory, globalPresentersFactory, mainMenuUIRoot);
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer container)
        {
            MainMenuUIRoot UIRoot = container.Resolve<MainMenuUIRoot>();

            ViewsFactory factory = container.Resolve<ViewsFactory>();
            MainMenuScreenView mainMenuScreenView = factory.Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, UIRoot.HUDLayer);

            MainMenuPresentersFactory presentersFactory = container.Resolve<MainMenuPresentersFactory>();

            return presentersFactory.CreateMainMenuScreenPresenter(mainMenuScreenView);
        }

        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer container)
        {
            return new MainMenuPresentersFactory(container);
        }

        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer container)
        {
            MainMenuUIRoot mainMenuUIRootPrefab = Resources.Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");
            return Object.Instantiate(mainMenuUIRootPrefab);
        }

        private static MainMenu CreateMainMenu(DIContainer container)
        {
            SceneSwitcherService sceneSwitcher = container.Resolve<SceneSwitcherService>();
            ConfigProviderService configsProvider = container.Resolve<ConfigProviderService>();
            ICoroutinesRunner coroutinesRunner = container.Resolve<ICoroutinesRunner>();
            WalletService walletService = container.Resolve<WalletService>();
            StatsService statsService = container.Resolve<StatsService>();
            MarketService marketService = container.Resolve<MarketService>();

            return new MainMenu(
                sceneSwitcher,
                configsProvider,
                coroutinesRunner,
                statsService,
                walletService,
                marketService);
        }

        private static MainMenuControllersFactory CreateMainMenuControllersFactory(DIContainer container)
        {
            return new MainMenuControllersFactory();
        }
    }
}