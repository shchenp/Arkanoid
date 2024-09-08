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
    
    private readonly IDisposable _subscription;

    public PrefsManager(ISubscriber<UpdateDataMessage> dataSubscriber)
    {
        _subscription = dataSubscriber.Subscribe(message =>
            SaveData(message.GameData.Score, message.GameData.Lives, message.GameData.LevelName));
    }

    private void SaveData(int score, int lives, string levelName)
    {
        PlayerPrefs.SetInt(SCORE_DATA_KEY, score);
        PlayerPrefs.SetInt(LIVES_DATA_KEY, lives);
        PlayerPrefs.SetString(LEVEL_DATA_KEY, levelName);
        
        PlayerPrefs.Save();
    }
    
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public bool HasData()
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
        _subscription?.Dispose();
    }
}