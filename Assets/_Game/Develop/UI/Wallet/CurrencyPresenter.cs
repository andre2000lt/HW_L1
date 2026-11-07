#region

using System;
using _Game.Develop.Gameplay;
using _Game.Develop.IReadOnlyReactVariable;
using _Game.Develop.Meta.Features;
using _Game.Develop.UI.CommonView;
using _Game.Develop.UI.Core;

#endregion

namespace _Game.Develop.UI.Wallet
{
    public class CurrencyPresenter : IPresenter
    {
        private readonly IReadOnlyReactVariable<int> _currencyAmount;
        private readonly CurrencyType _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        private readonly IconTextView _iconTextView;

        private IDisposable _disposable;

        public CurrencyPresenter(
            IReadOnlyReactVariable<int> currencyAmount,
            CurrencyType currencyType,
            CurrencyIconsConfig currencyIconsConfig,
            IconTextView iconTextView)
        {
            _currencyAmount = currencyAmount;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _iconTextView = iconTextView;
        }

        public IconTextView View => _iconTextView;

        public void Initialize()
        {
            UpdateText();
            _iconTextView.SetImage(_currencyIconsConfig.GetSpriteFor(_currencyType));
            _disposable = _currencyAmount.Subscribe(OnAmountChanged);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void OnAmountChanged(int oldValue, int newValue)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _iconTextView.SetText(_currencyAmount.Value.ToString());
        }
    }
}