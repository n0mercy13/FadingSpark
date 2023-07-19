using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Codebase.Infrastructure;

namespace Codebase.Services.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoaderService(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public static string CurrentLevelName => 
            SceneManager.GetActiveScene().name;

        public void Load(string sceneName, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if(CurrentLevelName == nextScene)
            {
                onLoaded?.Invoke();

                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            yield return new WaitUntil(() => waitNextScene.isDone);

            onLoaded?.Invoke();
        }
    }
}