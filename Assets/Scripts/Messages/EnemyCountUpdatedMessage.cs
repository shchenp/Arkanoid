namespace Messages
{
    public struct EnemyCountUpdatedMessage
    {
        public int AliveCount { get; private set; }
        public int TotalCount { get; private set; }

        public EnemyCountUpdatedMessage(int alive, int total)
        {
            AliveCount = alive;
            TotalCount = total;
        }
    }
}