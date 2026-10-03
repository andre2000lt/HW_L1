using _Game.Develop.Utils.ControllersManagement;
using UnityEngine;

namespace _Game.Develop.Meta
{
    public class MainMenuController : Controller
    {
        private MainMenu _mainMenu;

        public MainMenuController(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            HandleKeyPressed(KeyCode.Alpha1);
            HandleKeyPressed(KeyCode.Alpha2);
        }

        private void HandleKeyPressed(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                _mainMenu.StartGame(key);
            }
        }
    }
}