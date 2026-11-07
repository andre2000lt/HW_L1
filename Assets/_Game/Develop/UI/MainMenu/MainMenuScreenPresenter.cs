#region

using System.Collections.Generic;
using _Game.Develop.UI.CommonView;
using _Game.Develop.UI.Core;
using _Game.Develop.UI.Wallet;

#endregion

namespace _Game.Develop.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _mainMenuScreenView;
        private readonly GlobalPresentersFactory _globalPresentersFactory;
        private MainMenuPopupService _popupService;

        private readonly List<IPresenter> _walletPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView mainMenuScreenView,
            GlobalPresentersFactory globalPresentersFactory,
            MainMenuPopupService popupService
        )
        {
            _mainMenuScreenView = mainMenuScreenView;
            _globalPresentersFactory = globalPresentersFactory;
            _popupService = popupService;
        }

        public void Initialize()
        {
            _mainMenuScreenView.OpenTestPopupButtonClicked += OnOpenTestPopupButtonClicked;

            CreateWalletPresenter();

            foreach (IPresenter presenter in _walletPresenters)
            {
                presenter.Initialize();
            }
        }

        public void Dispose()
        {
            _mainMenuScreenView.OpenTestPopupButtonClicked -= OnOpenTestPopupButtonClicked;

            foreach (IPresenter presenter in _walletPresenters)
            {
                presenter.Dispose();
            }

            _walletPresenters.Clear();
        }

        private void CreateWalletPresenter()
        {
            IconTextListView walletView = _mainMenuScreenView.WalletView;
            WalletPresenter walletPresenter =
                _globalPresentersFactory.CreateWalletPresenter(walletView);

            _walletPresenters.Add(walletPresenter);
        }

        private void OnOpenTestPopupButtonClicked()
        {
            _popupService.OpenTestPopup();
        }
    }
}