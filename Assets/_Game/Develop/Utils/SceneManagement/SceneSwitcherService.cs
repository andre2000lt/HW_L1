using System;
using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _MiniGame;
using Object = UnityEngine.Object;

namespace _Game.Develop.Utils.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly DIContainer _globalContainer;
        private readonly ILoadingScreen _loadingScreen;
        private readonly SceneLoaderService _sceneLoaderService;

        public SceneSwitcherService(DIContainer globalContainer)
        {
            _globalContainer = globalContainer;

            _loadingScreen = _globalContainer.Resolve<ILoadingScreen>();
            _sceneLoaderService = _globalContainer.Resolve<SceneLoaderService>();
        }

        public IEnumerator ProcessSwitchTo(SceneName sceneName, ISceneData sceneData = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(SceneName.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap bootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (bootstrap == null)
                throw new Exception($"Can't find {sceneName.ToString()} scene bootstrap");

            DIContainer sceneContainer = new(_globalContainer);
            bootstrap.ProcessRegistrations(sceneContainer, sceneData);

            yield return bootstrap.Initialize();

            _loadingScreen.Hide();

            bootstrap.Run();
        }
    }
}