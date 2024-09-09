using System;
using MessagePipe;
using Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class LevelManager : MonoBehaviour
{
    private IDisposable _gameOverSubscription;

    [Inject]
    private void Construct(ISubscriber<GameOverMessage> gameOverSubscriber)
    {
        _gameOverSubscription = gameOverSubscriber.Subscribe(_ => LoadGameOverScene());
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE_NAME, LoadSceneMode.Additive);
    }

    private void OnDestroy()
    {
        _gameOverSubscription?.Dispose();
    }
}