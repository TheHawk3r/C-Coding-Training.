using System;

namespace SoccerRanking
{
    class SoccerTeam : IComparable<SoccerTeam>
    {
        readonly string name;
        readonly int points;

        public SoccerTeam(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public int CompareTo(SoccerTeam teamToCompareTo)
        {
            if (teamToCompareTo == null)
            {
                return 1;
            }

            return teamToCompareTo.points.CompareTo(this.points);
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetPoints()
        {
            return this.points;
        }
    }
}
