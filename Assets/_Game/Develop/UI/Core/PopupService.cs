#region

using System;
using System.Collections.Generic;
using _Game.Develop.UI.Core.TestPopup;
using UnityEngine;

#endregion

namespace _Game.Develop.UI.Core
{
    public abstract class PopupService : IDisposable
    {
        protected readonly ViewsFactory _viewsFactory;
        protected readonly Dictionary<PopupPresenterBase, popupPresentersInfo> _presenterToInfo = new();

        private readonly GlobalPresentersFactory _globalPresentersFactory;

        protected PopupService(ViewsFactory viewsFactory, GlobalPresentersFactory globalPresentersFactory)
        {
            _viewsFactory = viewsFactory;
            _globalPresentersFactory = globalPresentersFactory;
        }

        protected abstract Transform PopupLayer { get; }

        public TestPopupPresenter OpenTestPopup(Action closeCallback = null)
        {
            TestPopupView popupView = _viewsFactory.Create<TestPopupView>(ViewIDs.TestPopup, PopupLayer);
            TestPopupPresenter popupPresenter = _globalPresentersFactory.CreateTestPopupPresenter(popupView);

            OnPopupCreated(popupPresenter, popupView, closeCallback);

            return popupPresenter;
        }

        public void ClosePopup(PopupPresenterBase popupPresenter)
        {
            popupPresenter.CloseRequested -= ClosePopup;

            //Debug.Log("Hide Popup");

            popupPresenter.Hide(() =>
            {
                _presenterToInfo[popupPresenter].CloseCallback?.Invoke();

                DisposePopup(popupPresenter);

                _presenterToInfo.Remove(popupPresenter);
            });
        }

        public void Dispose()
        {
            foreach (PopupPresenterBase popupPresenter in _presenterToInfo.Keys)
            {
                popupPresenter.CloseRequested -= ClosePopup;

                DisposePopup(popupPresenter);
            }

            _presenterToInfo.Clear();
        }

        protected void OnPopupCreated(
            PopupPresenterBase popupPresenter,
            PopupViewBase popupView,
            Action closeCallback = null
        )
        {
            popupPresenter.Initialize();
            popupPresenter.Show();

            popupPresentersInfo presentersInfo = new(popupView, closeCallback);
            _presenterToInfo.Add(popupPresenter, presentersInfo);

            popupPresenter.CloseRequested += ClosePopup;
        }

        private void DisposePopup(PopupPresenterBase popupPresenter)
        {
            _viewsFactory.Release(_presenterToInfo[popupPresenter].PopupView);
            popupPresenter.Dispose();
        }
    }

    public class popupPresentersInfo
    {
        public popupPresentersInfo(PopupViewBase popupView, Action closeCallback)
        {
            PopupView = popupView;
            CloseCallback = closeCallback;
        }

        public PopupViewBase PopupView { get; }
        public Action CloseCallback { get; }
    }
}