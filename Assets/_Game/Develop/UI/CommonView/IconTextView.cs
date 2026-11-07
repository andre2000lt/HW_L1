using _Game.Develop.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Develop.UI.CommonView
{
    public class IconTextView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}