using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ControllersManagement;
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
            _mainMenu = new MainMenu(_container);

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