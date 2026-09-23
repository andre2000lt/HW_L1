using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Develop.Utils.SceneManagement
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName.ToString(), loadSceneMode);

            if (operation == null)
                throw new Exception("Failed to load scene " + sceneName);

            yield return new WaitUntil(() => operation.isDone);
        }

        public IEnumerator UnLoadAsync(SceneName sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName.ToString());

            if (operation == null)
                throw new Exception("Failed to unload scene " + sceneName);

            yield return new WaitUntil(() => operation.isDone);
        }
    }
}