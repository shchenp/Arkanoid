using System;
using Interfaces;
using MessagePipe;
using Messages;
using UnityEngine;

public class PrefsManager : IDisposable, IDataContainer
{
    private const string SCORE_DATA_KEY = "Score";
    private const string LIVES_DATA_KEY = "Lives";
    private const string LEVEL_DATA_KEY = "Level";
    
    private readonly IDisposable _dataSubscription;
    private readonly IDisposable _gameOverSubscription;

    public PrefsManager(ISubscriber<UpdateDataMessage> dataSubscriber,
        ISubscriber<GameOverMessage> gameOverSubscriber)
    {
        _dataSubscription = dataSubscriber.Subscribe(message =>
            SaveData(message.GameData.Score, message.GameData.Lives, message.GameData.LevelName));

        _gameOverSubscription = gameOverSubscriber.Subscribe(message => SaveData(message.Score));
    }

    private void SaveData(int score, int lives = 0, string levelName = null)
    {
        PlayerPrefs.SetInt(SCORE_DATA_KEY, score);
        PlayerPrefs.SetInt(LIVES_DATA_KEY, lives);

        if (levelName != null)
        {
            PlayerPrefs.SetString(LEVEL_DATA_KEY, levelName);
        }
        else
        {
            PlayerPrefs.DeleteKey(LEVEL_DATA_KEY);
        }
        
        PlayerPrefs.Save();
    }
    
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public bool HasLevelData()
    {
        return PlayerPrefs.HasKey(LEVEL_DATA_KEY);
    }
    
    public string GetLevelName()
    {
        return PlayerPrefs.GetString(LEVEL_DATA_KEY);
    }

    public int GetLives()
    {
        return PlayerPrefs.GetInt(LIVES_DATA_KEY);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt(SCORE_DATA_KEY);
    }

    public void Dispose()
    {
        _dataSubscription?.Dispose();
        _gameOverSubscription?.Dispose();
    }
}