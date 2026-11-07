#region

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace _Game.Develop.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequested;

        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Image _antiClicker;

        private Tween _currentAnimation;

        private void Awake()
        {
            _mainCanvasGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            KillCurrentAnimation();
        }

        public void OnCloseButtonClicked()
        {
            CloseRequested?.Invoke();
        }

        public Tween Show()
        {
            KillCurrentAnimation();

            OnPreShow();

            _mainCanvasGroup.alpha = 1;

            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_antiClicker
                    .DOFade(0.8f, 0.4f)
                    .From(0))
                .Join(_bodyTransform
                    .DOScale(1, 1f)
                    .From(0)
                    .SetEase(Ease.OutBack));

            ModifyShowAnimation(sequence);

            sequence.OnComplete(OnPostShow);

            return _currentAnimation = sequence.SetUpdate(true).Play();
        }

        public Tween Hide()
        {
            KillCurrentAnimation();

            OnPreHide();

            _mainCanvasGroup.alpha = 0;

            Sequence sequence = DOTween.Sequence();

            ModifyHideAnimation(sequence);

            sequence.OnComplete(OnPostHide);

            return _currentAnimation = sequence.SetUpdate(true).Play();
        }

        protected virtual void ModifyShowAnimation(Sequence sequence)
        {
        }

        protected virtual void ModifyHideAnimation(Sequence animation)
        {
        }

        protected virtual void OnPreShow()
        {
        }

        protected virtual void OnPostShow()
        {
        }

        protected virtual void OnPreHide()
        {
        }

        protected virtual void OnPostHide()
        {
        }

        private void KillCurrentAnimation()
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Kill();
            }
        }
    }
}