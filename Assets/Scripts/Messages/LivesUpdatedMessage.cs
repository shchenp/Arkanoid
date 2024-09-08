namespace Messages
{
    public struct LivesUpdatedMessage
    {
        public int Lives { get; private set; }

        public LivesUpdatedMessage(int lives)
        {
            Lives = lives;
        }
    }
}