using MessagePipe;
using Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace UI.MainMenuScreen
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IPublisher<NewGameStartedMessage> _publisher;

        [Inject]
        private void Construct(IPublisher<NewGameStartedMessage> newGamePublisher)
        {
           _publisher = newGamePublisher;
        }

        private void Awake()
        {
            _button.onClick.AddListener(StartNewGame);
        }

        private void StartNewGame()
        {
            _publisher.Publish(new NewGameStartedMessage());
            
            SceneManager.LoadSceneAsync(GlobalConstants.LEVEL_SCENE_NAME);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(StartNewGame);
        }
    }
}