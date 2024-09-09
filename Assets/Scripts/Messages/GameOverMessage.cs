namespace Messages
{
    public struct GameOverMessage
    {
        public int Score { get; private set; }

        public GameOverMessage(int score)
        {
            Score = score;
        }
    }
}