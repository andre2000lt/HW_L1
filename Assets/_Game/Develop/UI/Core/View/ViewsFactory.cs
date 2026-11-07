#region

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace _Game.Develop.UI.Core
{
    public class ViewsFactory
    {
        private Dictionary<string, string> _viewIdToPath = new()
        {
            { ViewIDs.CurrencyView, "UI/Wallet/CurrencyView" },
            { ViewIDs.MainMenuScreen, "UI/MainMenu/MainMenuScreen" },
            { ViewIDs.TestPopup, "UI/TestPopup" }
        };

        public TView Create<TView>(string viewId, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIdToPath.TryGetValue(viewId, out string path) == false)
            {
                throw new Exception($"View with ID({viewId}) doesn't exist");
            }

            GameObject viewGameObject = Resources.Load<GameObject>(path);
            GameObject instance = Object.Instantiate(viewGameObject, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
            {
                throw new Exception($"View with type({typeof(TView)}) doesn't exist in instance");
            }

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}