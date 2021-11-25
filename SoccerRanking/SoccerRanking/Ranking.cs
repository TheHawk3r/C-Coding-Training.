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

        public SoccerTeam GetTeamOnPosition(int position)
        {
            return teams[position - 1];
        }

        public int GetPositionOfTeam(SoccerTeam team)
        {
            return teams.FindIndex(x => x.Equals(team)) + 1;
        }

        public void UpdateRankingBasedOnMatch(SoccerTeam firstTeam, int firstTeamScore, int secondTeamScore, SoccerTeam secondTeam)
        {
            int firstTeamIndex = teams.FindIndex(x => x.Equals(firstTeam));
            int secondTeamIndex = teams.FindIndex(x => x.Equals(secondTeam));

            if (firstTeamIndex == -1 || secondTeamIndex == -1)
            {
                return;
            }

            if (firstTeamScore > secondTeamScore)
            {
                firstTeam.AddWonMatch();
                secondTeam.AddLostMatch();
            }
            else if (firstTeamScore == secondTeamScore)
            {
                firstTeam.AddDraw();
                secondTeam.AddDraw();
            }
            else
            {
                secondTeam.AddWonMatch();
                firstTeam.AddLostMatch();
            }

            teams.Sort();
        }

        private List<SoccerTeam> GetTeams()
        {
            return this.teams;
        }
    }
}
