using System.Collections.Generic;

namespace SoccerRanking
{
    class Ranking
    {
        readonly List<SoccerTeam> teams;

        public Ranking(List<SoccerTeam> teams)
        {
            this.teams = teams;
            teams.Sort();
        }

        public void AddTeam(SoccerTeam team)
        {
            teams.Add(team);
            teams.Sort();
        }

        public string GetTeamOnPosition(int position)
        {
            return teams[position - 1].GetName();
        }

        public int GetPositionOfTeam(string nameOfTeam)
        {
            return teams.FindIndex(x => x.GetName().Contains(nameOfTeam)) + 1;
        }
    }
}
