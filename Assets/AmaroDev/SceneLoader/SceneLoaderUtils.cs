
using UnityEngine.SceneManagement;
namespace AmaroDev.SceneManagement
{
    public static class SceneLoaderUtils
    {
        public static bool Load(this SceneLoader.ScenesList scene)
        {
            if (scene == SceneLoader.ScenesList.PreviuousScene)
            {
                return SceneLoader.instance.PreviuousScene();

            }
            else if (scene == SceneLoader.ScenesList.NextScene)
            {
                return SceneLoader.instance.NextScene();
            }
            else
            {
                SceneLoader.instance.Load(scene);
            }
            return true;
        }

        public static int GetIndex(this SceneLoader.ScenesList scene)
        {
            return (int)scene;
        }

        public static Scene GetScene(SceneLoader.ScenesList scene)
        {
            return SceneManager.GetSceneByBuildIndex(scene.GetIndex());
        }
    }
}