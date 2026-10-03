using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Develop.Utils
{
    public static class KeyCodeConverter
    {
        private static Dictionary<KeyCode, int> _keyCodeToInt = new()
        {
            { KeyCode.Alpha0, 0 },
            { KeyCode.Alpha1, 1 },
            { KeyCode.Alpha2, 2 },
            { KeyCode.Alpha3, 3 },
            { KeyCode.Alpha4, 4 },
            { KeyCode.Alpha5, 5 },
            { KeyCode.Alpha6, 6 },
            { KeyCode.Alpha7, 7 },
            { KeyCode.Alpha8, 8 },
            { KeyCode.Alpha9, 9 }
        };

        public static KeyCode FromChar(char symbol)
        {
            if (char.IsDigit(symbol))
            {
                int digit = int.Parse(symbol.ToString());
                KeyCode keyCode = _keyCodeToInt.First(x => x.Value == digit).Key;
                return keyCode;
            }

            return (KeyCode)Enum.Parse(typeof(KeyCode), symbol.ToString());
        }

        public static int ToInt(KeyCode keyCode)
        {
            return _keyCodeToInt[keyCode];
        }
    }
}