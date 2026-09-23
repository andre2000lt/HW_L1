using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
                ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();

                coroutinesRunner.Perform(sceneSwitcher.ProcessSwitchTo(SceneName.MainMenu));
            }
        }

        public override IEnumerator Initialize(DIContainer container)
        {
            _container = container;

            yield return new WaitForSeconds(0.5f);

            Debug.Log("Gameplay Bootstrap Initialized");
        }

        public override void Run()
        {
            Debug.Log("Gameplay Bootstrap Run");
        }
    }
}