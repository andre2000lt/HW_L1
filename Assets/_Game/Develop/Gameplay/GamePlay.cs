using _Game.Develop.Utils.ConfigServices.Configs;
using UnityEngine;

namespace _Game.Develop.Gameplay
{
    public class GamePlay
    {
        private const int wordLength = 5;

        private string _word;
        private int _currentIndex;

        public GameStatus GameStatus { get; private set; }
        public string ValidSymbols { get; }

        public GamePlay(LevelConfig levelConfig)
        {
            ValidSymbols = levelConfig.ValidSymbols;
        }

        public void StartGame()
        {
            _word = GenerateString();
            Debug.Log(_word);

            _currentIndex = 0;

            GameStatus = GameStatus.Running;
        }

        public void CheckSymbol(char symbol)
        {
            if (_currentIndex >= _word.Length) return;

            Debug.Log(symbol);

            if (symbol == _word[_currentIndex])
            {
                if (_currentIndex == _word.Length - 1)
                {
                    GameStatus = GameStatus.Won;
                    return;
                }

                _currentIndex++;
            }
            else
            {
                GameStatus = GameStatus.Lost;
            }
        }

        private string GenerateString()
        {
            string word = "";

            for (int i = 0; i < wordLength; i++)
            {
                int index = Random.Range(0, ValidSymbols.Length);
                string symbol = ValidSymbols[index].ToString();
                word += symbol;
            }

            return word;
        }
    }
}