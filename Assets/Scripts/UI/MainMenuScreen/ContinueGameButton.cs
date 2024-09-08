using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace UI.MainMenuScreen
{
    public class ContinueGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IDataContainer _dataContainer;

        [Inject]
        private void Construct(IDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }
        
        private void Awake()
        {
            _button.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadSceneAsync(_dataContainer.GetLevelName());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadScene);
        }
    }
}