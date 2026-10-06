using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ControllersManagement;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private MainMenu _mainMenu;
        private MainMenuController _mainMenuController;

        public override void ProcessRegistrations(DIContainer container, ISceneData sceneData)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            ConfigProviderService configsProvider = _container.Resolve<ConfigProviderService>();
            ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();

            _mainMenu = new MainMenu(sceneSwitcher, configsProvider, coroutinesRunner);

            MainMenuControllersFactory controllersFactory = _container.Resolve<MainMenuControllersFactory>();
            _mainMenuController = controllersFactory.CreateMainMenuController(_mainMenu);

            yield return new WaitForSeconds(0.5f);

            Debug.Log("MainMenu Bootstrap Initialized");
        }

        public override void Run()
        {
            _mainMenuController.Enable();
        }

        private void Update()
        {
            _mainMenuController?.Update(Time.deltaTime);
        }
    }
}