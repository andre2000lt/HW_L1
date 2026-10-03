using _Game.Develop.Utils;
using _Game.Develop.Utils.ControllersManagement;
using UnityEngine;

namespace _Game.Develop.Gameplay
{
    public class GamePlayController : Controller
    {
        private GamePlay _gamePlay;

        public GamePlayController(GamePlay gamePlay)
        {
            _gamePlay = gamePlay;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_gamePlay.GameStatus != GameStatus.Running) return;

            for (int i = 0; i < _gamePlay.ValidSymbols.Length; i++)
            {
                KeyCode key = KeyCodeConverter.FromChar(_gamePlay.ValidSymbols[i]);

                if (Input.GetKeyDown(key))
                {
                    string str = key.ToString();
                    char symbol = str[str.Length - 1];

                    _gamePlay.CheckSymbol(symbol);
                }
            }
        }
    }
}