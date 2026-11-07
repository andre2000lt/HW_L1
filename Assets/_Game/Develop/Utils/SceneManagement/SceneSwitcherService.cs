#region

using System;
using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _MiniGame;
using Object = UnityEngine.Object;

#endregion

namespace _Game.Develop.Utils.SceneManagement
{
    public class SceneSwitcherService
    {
        private readonly DIContainer _globalContainer;
        private readonly ILoadingScreen _loadingScreen;
        private readonly SceneLoaderService _sceneLoaderService;

        private DIContainer _sceneContainer;

        public SceneSwitcherService(DIContainer globalContainer)
        {
            _globalContainer = globalContainer;

            _loadingScreen = _globalContainer.Resolve<ILoadingScreen>();
            _sceneLoaderService = _globalContainer.Resolve<SceneLoaderService>();
        }

        public IEnumerator ProcessSwitchTo(SceneName sceneName, ISceneData sceneData = null)
        {
            _loadingScreen.Show();

            _sceneContainer?.Dispose();

            yield return _sceneLoaderService.LoadAsync(SceneName.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            SceneBootstrap bootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (bootstrap == null)
                throw new Exception($"Can't find {sceneName.ToString()} scene bootstrap");

            _sceneContainer = new DIContainer(_globalContainer);
            bootstrap.ProcessRegistrations(_sceneContainer, sceneData);
            _sceneContainer.Initialize();

            yield return bootstrap.Initialize();

            _loadingScreen.Hide();

            bootstrap.Run();
        }
    }
}