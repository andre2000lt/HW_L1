using _Game.Develop.Utils.ControllersManagement;
using UnityEngine;

namespace _Game.Develop.Meta
{
    public class MainMenuController : Controller
    {
        private readonly MainMenu _mainMenu;

        public MainMenuController(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            HandleStartGameKeyPressed(KeyCode.Alpha1);
            HandleStartGameKeyPressed(KeyCode.Alpha2);
            HandleShowStatsKeyPressed(KeyCode.Alpha3);
            HandleResetStatsKeyPressed(KeyCode.Alpha4);
        }

        private void HandleStartGameKeyPressed(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                _mainMenu.StartGame(key);
            }
        }

        private void HandleShowStatsKeyPressed(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                _mainMenu.ShowStats();
            }
        }

        private void HandleResetStatsKeyPressed(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                _mainMenu.ResetStats();
            }
        }
    }
}