namespace SoccerRanking
{
    class SoccerTeam
    {
        readonly string name;
        readonly int points;

        public SoccerTeam(string name, int points)
        {
            this.name = name;
            this.points = points;
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
