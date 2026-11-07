#region

using DG.Tweening;

#endregion

namespace _Game.Develop.UI.Core
{
    public interface IShowableView : IView
    {
        Tween Show();

        Tween Hide();
    }
}