using System;
using Interfaces;
using MessagePipe;
using Messages;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace UI.MainMenuScreen
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private ContinueGameButton _continueGameButton;

        private IDataContainer _dataContainer;
        private GameData _newGameData;
        private IDisposable _subscription;
        private IPublisher<UpdateDataMessage> _updatePublisher;

        [Inject]
        private void Construct(IDataContainer dataContainer, 
            GameData newGameData, 
            ISubscriber<NewGameStartedMessage> newGameStartedSubscriber,
            IPublisher<UpdateDataMessage> updatePublisher)
        {
            _dataContainer = dataContainer;
            _newGameData = newGameData;
            _subscription = newGameStartedSubscriber.Subscribe(_ => SendMessage());
            _updatePublisher = updatePublisher;
        }

        private void Start()
        {
            if (_dataContainer.HasData())
            {
                _continueGameButton.gameObject.SetActive(true);
            }
        }

        private void SendMessage()
        {
            _updatePublisher.Publish(new UpdateDataMessage(_newGameData));
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}