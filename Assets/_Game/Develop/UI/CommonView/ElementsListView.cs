using System.Collections.Generic;
using _Game.Develop.UI.Core;
using UnityEngine;

namespace _Game.Develop.UI.CommonView
{
    public class ElementsListView<TElement> : MonoBehaviour, IView where TElement : MonoBehaviour, IView
    {
        [SerializeField] private Transform _viewsParent;

        private readonly List<TElement> _elements = new();

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.transform.SetParent(_viewsParent);
            _elements.Add(element);
        }

        public void Remove(TElement element)
        {
            element.transform.SetParent(null);
            _elements.Remove(element);
        }
    }
}