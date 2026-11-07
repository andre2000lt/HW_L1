#region

using DG.Tweening;
using TMPro;
using UnityEngine;

#endregion

namespace _Game.Develop.UI.Core.TestPopup
{
    public class TestPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _title;

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        protected override void ModifyShowAnimation(Sequence sequence)
        {
            base.ModifyShowAnimation(sequence);

            sequence
                .Append(_title
                    .DOFade(1, 0.5f)
                    .From(0));
        }
    }
}