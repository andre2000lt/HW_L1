namespace _MiniGame
{
    public interface ILoadingScreen
    {
        bool IsShowing { get; }
        void Show();
        void Hide();
    }
}