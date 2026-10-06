using _Game.Develop.Gameplay.Infrastructure;
using _Game.Develop.Utils;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Meta
{
    public class MainMenu
    {
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesRunner _coroutinesRunner;

        private readonly LevelConfigs _levelConfigs;

        public MainMenu(
            SceneSwitcherService sceneSwitcher,
            ConfigProviderService configProvider,
            ICoroutinesRunner coroutinesRunner
        )
        {
            _sceneSwitcher = sceneSwitcher;
            _coroutinesRunner = coroutinesRunner;

            _levelConfigs = configProvider.GetConfig<LevelConfigs>();
        }

        public void StartGame(KeyCode keyCode)
        {
            int id = KeyCodeConverter.ToInt(keyCode);
            LevelConfig levelConfig = _levelConfigs.GetConfigBy(id);

            _coroutinesRunner.Perform(_sceneSwitcher.ProcessSwitchTo(SceneName.Gameplay, new GameplaySceneData(levelConfig)));
        }
    }
}