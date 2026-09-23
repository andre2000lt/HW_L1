using System.Collections;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using _MiniGame;
using UnityEngine;

namespace _Game.Develop.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DIContainer _container;

        private void Awake()
        {
            SetupAppSettings();

            _container = new DIContainer();
            EntryPointRegistrations.Process(_container);

            ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();
            Debug.Log(coroutinesRunner);
            coroutinesRunner.Perform(InitializeProcess());
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }


        private IEnumerator InitializeProcess()
        {
            ILoadingScreen loadingScreen = _container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            ConfigProviderService configsProvider = _container.Resolve<ConfigProviderService>();

            yield return configsProvider.LoadAsync();
            yield return new WaitForSeconds(3f);

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(SceneName.MainMenu);
        }
    }
}