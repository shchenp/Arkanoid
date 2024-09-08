namespace Messages
{
    public struct ScoreUpdatedMessage
    {
        public int Score { get; private set; }

        public ScoreUpdatedMessage(int score)
        {
            Score = score;
        }
    }
}