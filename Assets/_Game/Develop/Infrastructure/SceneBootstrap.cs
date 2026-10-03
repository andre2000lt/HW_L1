using System.Collections;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Infrastructure
{
    public abstract class SceneBootstrap : MonoBehaviour
    {
        public abstract void ProcessRegistrations(DIContainer container, ISceneData sceneData);
        public abstract IEnumerator Initialize();

        public abstract void Run();
    }
}