using System.Collections;
using _Game.Develop.Infrastructure.DI;
using UnityEngine;

namespace _Game.Develop.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract IEnumerator Initialize(DIContainer container);

        public abstract void Run();
    }
}