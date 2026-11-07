#region

using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Meta.Features;
using _Game.Develop.UI.CommonView;
using _Game.Develop.Utils.ControllersManagement;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

#endregion

namespace _Game.Develop.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private IconTextListView _iconTextListView;

        private DIContainer _container;

        private MainMenu _mainMenu;
        private MainMenuController _mainMenuController;

        private WalletService _walletService;

        public override void ProcessRegistrations(DIContainer container, ISceneData sceneData)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _mainMenu = _container.Resolve<MainMenu>();

            _walletService = _container.Resolve<WalletService>();

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

            if (Input.GetKeyDown(KeyCode.A))
            {
                _walletService.AddCurrency(CurrencyType.Gold, 10);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _walletService.Spend(CurrencyType.Gold, 10);
            }
        }
    }
}