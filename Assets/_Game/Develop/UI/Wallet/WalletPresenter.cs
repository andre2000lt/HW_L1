#region

using System.Collections.Generic;
using _Game.Develop.IReadOnlyReactVariable;
using _Game.Develop.Meta.Features;
using _Game.Develop.UI.CommonView;
using _Game.Develop.UI.Core;
using UnityEngine;

#endregion

namespace _Game.Develop.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly GlobalPresentersFactory _globalPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly IconTextListView _iconTextListView;

        private readonly List<CurrencyPresenter> _currencyPresenters = new();

        public WalletPresenter(
            WalletService walletService,
            GlobalPresentersFactory globalPresentersFactory,
            ViewsFactory viewsFactory,
            IconTextListView iconTextListView
        )
        {
            _walletService = walletService;
            _globalPresentersFactory = globalPresentersFactory;
            _viewsFactory = viewsFactory;
            _iconTextListView = iconTextListView;
        }

        public void Initialize()
        {
            foreach (CurrencyType currencyType in _walletService.CurrencyTypes)
            {
                IReadOnlyReactVariable<int> currencyAmount = _walletService.GetCurrencyAmount(currencyType);
                IconTextView iconTextView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);
                Debug.Log(iconTextView);

                CurrencyPresenter currencyPresenter =
                    _globalPresentersFactory.CreateCurrencyPresenter(
                        currencyAmount,
                        currencyType,
                        iconTextView
                    );

                _currencyPresenters.Add(currencyPresenter);
                _iconTextListView.Add(iconTextView);

                currencyPresenter.Initialize();
            }
        }

        public void Dispose()
        {
            foreach (CurrencyPresenter currencyPresenter in _currencyPresenters)
            {
                _iconTextListView.Remove(currencyPresenter.View);
                _viewsFactory.Release(currencyPresenter.View);
                currencyPresenter.Dispose();
            }

            _currencyPresenters.Clear();
        }
    }
}