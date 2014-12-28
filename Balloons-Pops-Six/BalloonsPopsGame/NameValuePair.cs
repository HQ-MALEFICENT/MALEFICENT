using System;

namespace BalloonsPopsGame
{
    public class NameValuePair : IComparable<NameValuePair>
    {
        public int moves;
        public string username;

        public NameValuePair(int moves, string username)
        {
            this.moves = moves;
            this.username = username;
        }

        public int CompareTo(NameValuePair other)
        {
            return this.moves.CompareTo(other.moves);
        }
    }
}