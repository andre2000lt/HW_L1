#region

using _Game.Develop.UI.Core;
using UnityEngine;

#endregion

namespace _Game.Develop.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;

        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            GlobalPresentersFactory globalPresentersFactory,
            MainMenuUIRoot uiRoot)
            : base(viewsFactory, globalPresentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
    }
}