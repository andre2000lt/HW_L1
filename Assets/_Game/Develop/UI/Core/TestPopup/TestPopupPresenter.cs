#region

using _Game.Develop.Utils.CorutinesManagement;

#endregion

namespace _Game.Develop.UI.Core.TestPopup
{
    public class TestPopupPresenter : PopupPresenterBase
    {
        private readonly TestPopupView _popupView;

        public TestPopupPresenter(TestPopupView popupView, ICoroutinesRunner coroutinesRunner) : base(coroutinesRunner)
        {
            _popupView = popupView;
        }

        protected override PopupViewBase PopupView => _popupView;

        public override void Initialize()
        {
            base.Initialize();

            _popupView.SetTitle("New Title");
        }
    }
}