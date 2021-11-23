using System;

namespace SoccerRanking
{
    class SoccerTeam : IComparable<SoccerTeam>
    {
        private const int PointsPerVictory = 3;
        readonly string name;
        readonly int matchesWon;
        readonly int matchesLost;
        readonly int drawMatches;

        public SoccerTeam(string name, int matchesWon, int matchesLost, int drawMatches)
        {
            this.name = name;
            this.matchesWon = matchesWon;
            this.matchesLost = matchesLost;
            this.drawMatches = drawMatches;
        }

        public int CompareTo(SoccerTeam teamToCompareTo)
        {
            if (teamToCompareTo == null)
            {
                return 1;
            }

            return teamToCompareTo.GetPoints().CompareTo(this.GetPoints());
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetPoints()
        {
            return this.matchesWon * PointsPerVictory + this.drawMatches;
        }

        public int GetMatchesPlayed()
        {
            return this.matchesWon + this.matchesLost + this.drawMatches;
        }
    }
}
