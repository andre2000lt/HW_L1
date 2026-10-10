using System.Collections;
using _Game.Develop.Infrastructure;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ControllersManagement;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;
using _Game.Develop.Utils.SceneManagement;
using UnityEngine;

namespace _Game.Develop.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private ICoroutinesRunner _coroutinesRunner;

        private MainMenu _mainMenu;
        private MainMenuController _mainMenuController;

        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvider;

        public override void ProcessRegistrations(DIContainer container, ISceneData sceneData)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            _walletService = _container.Resolve<WalletService>();
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();

            SceneSwitcherService sceneSwitcher = _container.Resolve<SceneSwitcherService>();
            ConfigProviderService configsProvider = _container.Resolve<ConfigProviderService>();
            _coroutinesRunner = _container.Resolve<ICoroutinesRunner>();

            _mainMenu = new MainMenu(sceneSwitcher, configsProvider, _coroutinesRunner);

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
            if (Input.GetKeyDown(KeyCode.A))
            {
                _walletService.AddCurrency(CurrencyType.Diamond, 10);
                Debug.Log("Diamond Count: " + _walletService.GetCurrencyAmount(CurrencyType.Diamond).Value);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _walletService.Spend(CurrencyType.Diamond, 10);
                Debug.Log("Diamond Count: " + _walletService.GetCurrencyAmount(CurrencyType.Diamond).Value);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log(Application.persistentDataPath);
                _coroutinesRunner.Perform(_playerDataProvider.Save());
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
            }
        }
    }
}