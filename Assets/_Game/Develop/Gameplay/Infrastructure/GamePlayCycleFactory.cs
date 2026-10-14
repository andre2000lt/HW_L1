using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Meta.Features;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using _Game.Develop.Utils.DataManagement.DataProviders;
using _Game.Develop.Utils.SceneManagement;

namespace _Game.Develop.Gameplay.Infrastructure
{
    public class GamePlayCycleFactory
    {
        private readonly DIContainer _container;

        public GamePlayCycleFactory(DIContainer container)
        {
            _container = container;
        }

        public GamePlayCycle CreateGamePlayCycle(LevelConfig levelConfig)
        {
            ICoroutinesRunner coroutinesRunner = _container.Resolve<ICoroutinesRunner>();
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            GameModeFactory gameModeFactory = _container.Resolve<GameModeFactory>();
            ConfigProviderService configProviderService = _container.Resolve<ConfigProviderService>();
            StatsService statsService = _container.Resolve<StatsService>();
            StatsDataProvider statsDataProvider = _container.Resolve<StatsDataProvider>();
            WalletService walletService = _container.Resolve<WalletService>();
            WalletDataProvider walletDataProvider = _container.Resolve<WalletDataProvider>();

            return new GamePlayCycle(
                levelConfig,
                gameModeFactory,
                coroutinesRunner,
                sceneSwitcherService,
                configProviderService,
                statsService,
                statsDataProvider,
                walletService,
                walletDataProvider
            );
        }
    }
}