using _Game.Develop.Gameplay.Infrastructure;
using _Game.Develop.Infrastructure.DI;
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
        private readonly DIContainer _container;


        public MainMenu(DIContainer container)
        {
            _container = container;
        }

        public void StartGame(KeyCode keyCode)
        {
            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            ConfigProviderService configsProvider = _container.Resolve<ConfigProviderService>();
            ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();

            LevelConfigs LevelConfigs = configsProvider.GetConfig<LevelConfigs>();

            int id = KeyCodeConverter.ToInt(keyCode);
            LevelConfig levelConfig = LevelConfigs.GetConfigBy(id);

            coroutinesRunner.Perform(sceneSwitcher.ProcessSwitchTo(SceneName.Gameplay, new GameplaySceneData(levelConfig)));
        }
    }
}