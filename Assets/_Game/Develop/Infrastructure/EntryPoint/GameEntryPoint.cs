using System.Collections;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;
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
            _globalContainer.Initialize();

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

            yield return ProcessInitializeWallet();
            yield return ProcessInitializeStats();

            yield return new WaitForSeconds(3f);

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(SceneName.MainMenu);
        }

        private IEnumerator ProcessInitializeWallet()
        {
            WalletDataProvider walletDataProvider = _globalContainer.Resolve<WalletDataProvider>();

            bool isWalletDataSaveExists = false;
            yield return walletDataProvider.IsExists(result => isWalletDataSaveExists = result);

            if (isWalletDataSaveExists)
                yield return walletDataProvider.Load();
            else
                walletDataProvider.Reset();
        }

        private IEnumerator ProcessInitializeStats()
        {
            StatsDataProvider statsDataProvider = _globalContainer.Resolve<StatsDataProvider>();

            bool isStatsDataSaveExists = false;
            yield return statsDataProvider.IsExists(result => isStatsDataSaveExists = result);

            if (isStatsDataSaveExists)
                yield return statsDataProvider.Load();
            else
                statsDataProvider.Reset();
        }
    }
}