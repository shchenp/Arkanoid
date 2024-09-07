using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(LoadLevelScene);
        }

        private void LoadLevelScene()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.MAIN_MENU_SCENE_NAME);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadLevelScene);
        }
    }
}