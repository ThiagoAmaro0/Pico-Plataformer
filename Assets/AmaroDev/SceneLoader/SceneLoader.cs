using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace AmaroDev.SceneManagement
{
    [DefaultExecutionOrder(-1)]
    public class SceneLoader : MonoBehaviour
    {
        private int _scenesCount;
        private ScenesList _currentScene;
        public static SceneLoader instance;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            _scenesCount = Enum.GetNames(typeof(ScenesList)).Length - 3;
            NextScene();
        }

        public async void Load(ScenesList scene)
        {
            if ((int)_currentScene > 0)
            {
                await SceneManager.UnloadSceneAsync(SceneLoaderUtils.GetIndex(_currentScene));
            }
            await SceneManager.LoadSceneAsync(SceneLoaderUtils.GetIndex(scene), LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneLoaderUtils.GetScene(scene));
            _currentScene = scene;
        }

        public bool PreviuousScene()
        {
            if ((int)_currentScene > 1)
            {
                ScenesList scene = (ScenesList)(SceneLoaderUtils.GetIndex(_currentScene) - 1);
                Load(scene);
                return true;
            }
            return false;
        }

        public bool NextScene()
        {
            if ((int)_currentScene < _scenesCount)
            {
                ScenesList scene = (ScenesList)(SceneLoaderUtils.GetIndex(_currentScene) + 1);
                Load(scene);
                return true;
            }
            return false;
        }

        public enum ScenesList
        {
            PreviuousScene = -2,
            NextScene = -1,
            BootLoader = 0,
            Menu = 1,
            SampleScene = 2,
        }
    }
}