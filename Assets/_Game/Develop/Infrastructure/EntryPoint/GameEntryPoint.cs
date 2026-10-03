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
        private DIContainer _globalContainer;

        private void Awake()
        {
            SetupAppSettings();

            _globalContainer = new DIContainer();
            GlobalRegistrations.Process(_globalContainer);

            ICoroutinesRunner coroutinesRunner = _globalContainer.Resolve<ICoroutinesRunner>();
            coroutinesRunner.Perform(InitializeProcess());
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }


        private IEnumerator InitializeProcess()
        {
            ILoadingScreen loadingScreen = _globalContainer.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = _globalContainer.Resolve<SceneSwitcherService>();

            loadingScreen.Show();

            ConfigProviderService configsProvider = _globalContainer.Resolve<ConfigProviderService>();

            yield return configsProvider.LoadAsync();
            yield return new WaitForSeconds(3f);

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(SceneName.MainMenu);
        }
    }
}