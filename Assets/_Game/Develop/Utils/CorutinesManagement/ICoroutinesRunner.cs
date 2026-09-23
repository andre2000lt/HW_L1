using System.Collections;
using UnityEngine;

namespace _Game.Develop.Utils.CorutinesManagement
{
    public interface ICoroutinesRunner
    {
        Coroutine Perform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}