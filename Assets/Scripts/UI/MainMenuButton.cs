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
            _button.onClick.AddListener(LoadMainMenuScene);
        }

        private void LoadMainMenuScene()
        {
            if (SceneManager.GetSceneByName(GlobalConstants.GAME_OVER_SCENE_NAME).isLoaded)
            {
                Debug.Log("Game Over Scene is loaded.");
                SceneManager.UnloadSceneAsync(GlobalConstants.GAME_OVER_SCENE_NAME);
            }
            
            SceneManager.LoadSceneAsync(GlobalConstants.MAIN_MENU_SCENE_NAME);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadMainMenuScene);
        }
    }
}