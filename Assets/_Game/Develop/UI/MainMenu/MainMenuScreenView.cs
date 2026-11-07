#region

using System;
using _Game.Develop.UI.CommonView;
using _Game.Develop.UI.Core;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace _Game.Develop.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenTestPopupButtonClicked;

        [field: SerializeField]
        public IconTextListView WalletView { get; private set; }

        [SerializeField] private Button _openTestPopupButton;

        private void OnEnable()
        {
            _openTestPopupButton.onClick.AddListener(OnOpenTestPopupButtonClicked);
        }

        private void OnDisable()
        {
            _openTestPopupButton.onClick.RemoveListener(OnOpenTestPopupButtonClicked);
        }

        private void OnOpenTestPopupButtonClicked()
        {
            OpenTestPopupButtonClicked?.Invoke();
        }
    }
}