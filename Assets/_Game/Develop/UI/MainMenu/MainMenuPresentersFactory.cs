#region

using _Game.Develop.Infrastructure.DI;

#endregion

namespace _Game.Develop.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreenPresenter(MainMenuScreenView mainMenuScreenView)
        {
            GlobalPresentersFactory presenterFactory = _container.Resolve<GlobalPresentersFactory>();
            MainMenuPopupService popupService = _container.Resolve<MainMenuPopupService>();

            return new MainMenuScreenPresenter(mainMenuScreenView, presenterFactory, popupService);
        }
    }
}