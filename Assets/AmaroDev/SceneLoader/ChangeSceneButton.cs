using UnityEngine;
using UnityEngine.UI;
namespace AmaroDev.SceneManagement
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneLoader.ScenesList _scene;
        private Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangeScene);
        }

        private void ChangeScene()
        {
            _scene.Load();
        }
    }
}