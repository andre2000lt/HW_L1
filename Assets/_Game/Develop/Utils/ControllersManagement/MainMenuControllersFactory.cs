using _Game.Develop.Meta;

namespace _Game.Develop.Utils.ControllersManagement
{
    public class MainMenuControllersFactory
    {
        public MainMenuController CreateMainMenuController(MainMenu mainMenu)
        {
            return new MainMenuController(mainMenu);
        }
    }
}