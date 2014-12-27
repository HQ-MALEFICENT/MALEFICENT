using System;

namespace BalloonsPopsGame
{
    public class NameValuePair : IComparable<NameValuePair>
    {
        public int val;
        public string name;

        public NameValuePair(int value, string name)
        {
            val = value;
            name = name;
        }

        public int CompareTo(NameValuePair other)
        {
            return val.CompareTo(other.val);
        }
    }
}