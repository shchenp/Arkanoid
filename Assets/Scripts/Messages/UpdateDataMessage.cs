using ScriptableObjects;

namespace Messages
{
    public struct UpdateDataMessage
    {
        public GameData GameData { get; private set; }

        public UpdateDataMessage(GameData gameData)
        {
            GameData = gameData;
        }
    }
}