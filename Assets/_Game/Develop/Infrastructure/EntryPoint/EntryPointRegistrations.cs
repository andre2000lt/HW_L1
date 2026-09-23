using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using _MiniGame;
using UnityEngine;

namespace _Game.Develop.Infrastructure.EntryPoint
{
    public static class EntryPointRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingleton<ICoroutinesRunner>(CreateCoroutineRunner);
            container.RegisterAsSingleton(CreateConfigProviderService);
            container.RegisterAsSingleton<IConfigsLoader>(CreateConfigLoader);
            container.RegisterAsSingleton<ILoadingScreen>(CreateLoadingScreen);
            container.RegisterAsSingleton(CreateSceneLoaderService);
            container.RegisterAsSingleton(CreateSceneSwitcherService);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer container)
        {
            SceneSwitcherService sceneSwitcherService = new(container);
            return new SceneSwitcherService(container);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer container)
        {
            return new SceneLoaderService();
        }

        private static CoroutinesesRunner CreateCoroutineRunner(DIContainer container)
        {
            CoroutinesesRunner coroutinesesRunnerPrefab = Resources.Load<CoroutinesesRunner>("Prefabs/CoroutineRunnner");
            CoroutinesesRunner coroutinesesRunner = Object.Instantiate(coroutinesesRunnerPrefab);

            return coroutinesesRunner;
        }

        private static ConfigProviderService CreateConfigProviderService(DIContainer container)
        {
            IConfigsLoader configsLoader = container.Resolve<IConfigsLoader>();
            ConfigProviderService configProviderService = new(configsLoader);

            return configProviderService;
        }

        private static ResourcesConfigsLoader CreateConfigLoader(DIContainer container)
        {
            ResourcesConfigsLoader configLoader = new();
            return configLoader;
        }

        private static LoadingScreen CreateLoadingScreen(DIContainer container)
        {
            LoadingScreen screenPrefab = Resources.Load<LoadingScreen>("Prefabs/LoadingScreen");
            LoadingScreen screen = Object.Instantiate(screenPrefab);

            return screen;
        }
    }
}