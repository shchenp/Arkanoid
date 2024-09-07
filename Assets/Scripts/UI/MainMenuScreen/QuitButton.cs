using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen
{
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(QuitGame);
        }
    }
}