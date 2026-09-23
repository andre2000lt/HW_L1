using System;
using System.Collections;
using System.Collections.Generic;

namespace _Game.Develop.Utils.ConfigServices
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigLoaded);
    }
}