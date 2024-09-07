using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenuScreen
{
    public class TutorialButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(LoadTutorialScene);
        }

        private void LoadTutorialScene()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.TUTORIAL_SCENE_NAME);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadTutorialScene);
        }
    }
}