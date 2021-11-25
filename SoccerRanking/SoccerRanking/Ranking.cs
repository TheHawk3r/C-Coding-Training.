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

        public void UpdateRankingBasedOnMatch(SoccerTeam firstTeam, string matchResult, SoccerTeam secondTeam)
        {
            int firstTeamIndex = teams.FindIndex(x => x.Equals(firstTeam));
            int secondTeamIndex = teams.FindIndex(x => x.Equals(secondTeam));
            string[] matchResultArray = matchResult.Split('-');

            if (firstTeamIndex == -1 || secondTeamIndex == -1 || matchResultArray.Length != 2)
            {
                return;
            }

            if (int.TryParse(matchResultArray[0], out int firstTeamScore) && int.TryParse(matchResultArray[1], out int secondTeamScore))
            {
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
            }
            else
            {
                return;
            }

            teams.Sort();
        }

        private List<SoccerTeam> GetTeams()
        {
            return this.teams;
        }
    }
}
