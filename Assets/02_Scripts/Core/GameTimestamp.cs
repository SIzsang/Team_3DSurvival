namespace _02_Scripts.Core
{
    public struct GameTimestamp
    {
        public int Day;
        public int Hour;
        public int Minute;

        public GameTimestamp(int day, int hour)
        {
            Day = day;
            Hour = hour;
            Minute = 0;
        }

        public GameTimestamp(int day, int hour, int minute)
        {
            Day = day;
            Hour = hour;
            Minute = minute;
        }

    }
}