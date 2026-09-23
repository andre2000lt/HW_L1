using System.Collections;
using UnityEngine;

namespace _Game.Develop.Utils.CorutinesManagement
{
    public class CoroutinesesRunner : MonoBehaviour, ICoroutinesRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Coroutine Perform(IEnumerator coroutineFunction)
        {
            return StartCoroutine(coroutineFunction);
        }

        public void StopPerform(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}