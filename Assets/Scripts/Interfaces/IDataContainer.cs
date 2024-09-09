namespace Interfaces
{
    public interface IDataContainer
    {
        bool HasLevelData();
        
        string GetLevelName();
        
        int GetLives();
        
        int GetScore();
    }
}