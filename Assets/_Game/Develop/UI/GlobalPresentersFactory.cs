#region

using _Game.Develop.Gameplay;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.IReadOnlyReactVariable;
using _Game.Develop.Meta.Features;
using _Game.Develop.UI.CommonView;
using _Game.Develop.UI.Core;
using _Game.Develop.UI.Core.TestPopup;
using _Game.Develop.UI.Wallet;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.CorutinesManagement;

#endregion

namespace _Game.Develop.UI
{
    public class GlobalPresentersFactory
    {
        private readonly DIContainer _container;

        public GlobalPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IReadOnlyReactVariable<int> currencyAmount,
            CurrencyType currencyType,
            IconTextView iconTextView)
        {
            ConfigProviderService configProviderService = _container.Resolve<ConfigProviderService>();
            CurrencyIconsConfig currencyIconsConfig = configProviderService.GetConfig<CurrencyIconsConfig>();

            return new CurrencyPresenter(
                currencyAmount,
                currencyType,
                currencyIconsConfig,
                iconTextView);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView iconTextView)
        {
            WalletService walletService = _container.Resolve<WalletService>();
            ViewsFactory viewsFactory = _container.Resolve<ViewsFactory>();

            return new WalletPresenter(
                walletService,
                this,
                viewsFactory,
                iconTextView
            );
        }

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView testPopupView)
        {
            ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();
            return new TestPopupPresenter(testPopupView, coroutinesRunner);
        }
    }
}