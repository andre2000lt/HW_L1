using System.Collections;
using _Game.Develop.Infrastructure.DI;
using _Game.Develop.Utils.ConfigServices;
using _Game.Develop.Utils.ConfigServices.Configs;
using _Game.Develop.Utils.CorutinesManagement;
using UnityEngine;

namespace _Game.Develop
{
    public class Test : MonoBehaviour
    {
        private Coroutine _coroutine;

        private DIContainer _container;
        private ICoroutinesRunner _coroutinesRunner;
        private ConfigProviderService _configProviderService;


        private void Awake()
        {
            _container = new DIContainer();


            _coroutinesRunner = _container.Resolve<ICoroutinesRunner>();
            _configProviderService = _container.Resolve<ConfigProviderService>();

            _coroutinesRunner.Perform(LoadConfigsProcess());
        }


        private IEnumerator LoadConfigsProcess()
        {
            Debug.Log("Load Configs Process");

            yield return _configProviderService.LoadAsync();

            Debug.Log("Configs Loaded");

            Debug.Log(_configProviderService.GetConfig<TestConfig>().Name);
        }
    }
}