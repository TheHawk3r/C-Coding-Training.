using System.Collections.Generic;

namespace SoccerRanking
{
    class Ranking
    {
        readonly List<SoccerTeam> teams;

        public Ranking(List<SoccerTeam> teams)
        {
            this.teams = teams;
        }

        public void AddTeam(SoccerTeam team)
        {
            teams.Add(team);
        }

        public string GetTeamOnPosition(int position)
        {
            return teams[position].GetName();
        }
    }
}
