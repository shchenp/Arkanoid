namespace Messages
{
    public struct EnemyDiedMessage
    {
        public int Points { get; private set; }

        public EnemyDiedMessage(int points)
        {
            Points = points;
        }
    }
}