using System;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.ControllersManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GameMode
    {
        public event Action GameWon;
        public event Action GameLost;

        private readonly GamePlay _gamePlay;
        private readonly GamePlayController _gamePlayController;

        private bool _isRunning;

        public GameMode(DIContainer container, LevelConfig levelConfig)
        {
            _gamePlay = new GamePlay(levelConfig);

            GamePlayControllersFactory controllersFactory = container.Resolve<GamePlayControllersFactory>();
            _gamePlayController = controllersFactory.CreateGamePlayController(_gamePlay);
        }

        public void Start()
        {
            _isRunning = true;
            _gamePlay.StartGame();
            _gamePlayController.Enable();
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false) return;

            _gamePlayController.Update(deltaTime);

            if (_gamePlay.GameStatus != GameStatus.Running)
            {
                if (_gamePlay.GameStatus == GameStatus.Won)
                    ProcessVictory();
                else
                    ProcessDefeat();
            }
        }

        private void ProcessVictory()
        {
            ProcessEndGame();

            GameWon?.Invoke();
        }

        private void ProcessDefeat()
        {
            ProcessEndGame();

            GameLost?.Invoke();
        }

        private void ProcessEndGame()
        {
            _gamePlayController.Disable();
            _isRunning = false;
        }
    }
}