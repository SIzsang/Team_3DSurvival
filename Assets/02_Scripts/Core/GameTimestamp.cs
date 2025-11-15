using System;

namespace _02_Scripts.Core
{
    [Serializable]
    public struct GameTimestamp : IEquatable<GameTimestamp>
    {
        public int day;
        public int hour;
        public int minute;

        public GameTimestamp(int day, int hour)
        {
            this.day = day;
            this.hour = hour;
            minute = 0;
        }

        public GameTimestamp(int day, int hour, int minute)
        {
            this.day = day;
            this.hour = hour;
            this.minute = minute;
        }

        public bool Equals(GameTimestamp other)
        {
            return this.day == other.day &&
                   this.hour == other.hour &&
                   this.minute == other.minute;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(day, hour, minute);
        }
    }
}