namespace Interfaces
{
    public interface IDataContainer
    {
        bool HasData();
        
        string GetLevelName();
        
        int GetLives();
        
        int GetScore();
    }
}