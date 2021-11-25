using System;

namespace SoccerRanking
{
    class SoccerTeam : IComparable<SoccerTeam>
    {
        private const int PointsPerVictory = 3;
        private readonly string name;
        private int points;
        private int matchesWon;
        private int matchesLost;
        private int drawMatches;

        public SoccerTeam(string name, int matchesWon, int matchesLost, int drawMatches)
        {
            this.name = name;
            this.matchesWon = matchesWon;
            this.matchesLost = matchesLost;
            this.drawMatches = drawMatches;
            this.points = this.matchesWon * PointsPerVictory + this.drawMatches;
        }

        public int CompareTo(SoccerTeam teamToCompareTo)
        {
            if (teamToCompareTo == null)
            {
                return 1;
            }

            return teamToCompareTo.points.CompareTo(this.points);
        }

        public bool Equals(SoccerTeam team)
        {
            if (team == null)
            {
                return false;
            }

            return this.name == team.name
                && this.drawMatches == team.drawMatches
                && this.matchesWon == team.matchesWon
                && this.matchesLost == team.matchesLost;
        }

        public void AddWonMatch()
        {
            this.matchesWon++;
            this.UpdatePoints();
        }

        public void AddLostMatch()
        {
            this.matchesLost++;
        }

        public void AddDraw()
        {
            this.drawMatches++;
            this.UpdatePoints();
        }

        private void UpdatePoints()
        {
            this.points = this.matchesWon * PointsPerVictory + drawMatches;
        }
    }
}
