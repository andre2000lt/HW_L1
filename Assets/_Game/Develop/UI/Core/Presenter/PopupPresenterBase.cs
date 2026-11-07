#region

using System;
using System.Collections;
using _Game.Develop.Utils.CorutinesManagement;
using DG.Tweening;
using UnityEngine;

#endregion

namespace _Game.Develop.UI.Core
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequested;

        private readonly ICoroutinesRunner _coroutinesRunner;
        private Coroutine _process;

        protected PopupPresenterBase(ICoroutinesRunner coroutinesRunner)
        {
            _coroutinesRunner = coroutinesRunner;
        }

        protected abstract PopupViewBase PopupView { get; }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
            KillProcess();

            PopupView.CloseRequested -= OnCloseRequested;
        }

        public void Show()
        {
            KillProcess();

            _coroutinesRunner.Perform(ProcessShow());
        }

        public void Hide(Action callback = null)
        {
            KillProcess();

            _coroutinesRunner.Perform(ProcessHide(callback));
        }

        protected virtual void OnPreShow()
        {
            PopupView.CloseRequested += OnCloseRequested;
        }

        protected virtual void OnPostShow()
        {
        }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequested -= OnCloseRequested;
        }

        protected virtual void OnPostHide()
        {
        }

        protected void OnCloseRequested()
        {
            CloseRequested?.Invoke(this);
            Debug.Log("Close Requested");
        }

        private IEnumerator ProcessShow()
        {
            OnPreShow();

            yield return PopupView.Show().WaitForCompletion();

            OnPostShow();
        }

        private IEnumerator ProcessHide(Action callback = null)
        {
            OnPreHide();

            yield return PopupView.Hide().WaitForCompletion();

            OnPostHide();

            callback?.Invoke();
        }

        private void KillProcess()
        {
            if (_process != null)
            {
                _coroutinesRunner.StopPerform(_process);
            }
        }
    }
}