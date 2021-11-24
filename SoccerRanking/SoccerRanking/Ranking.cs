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

        public List<SoccerTeam> GetTeams()
        {
            return this.teams;
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

        public void UpdateRankingBasedOnMatch(string firstTeamName, string matchResult, string secondTeamName)
        {
            int firstTeamIndex = teams.FindIndex(x => x.GetName().Contains(firstTeamName));
            int secondTeamIndex = teams.FindIndex(x => x.GetName().Contains(secondTeamName));
            string[] matchResultArray = matchResult.Split('-');

            if (firstTeamIndex == -1 || secondTeamIndex == -1 || matchResultArray.Length != 2)
            {
                return;
            }

            int firstTeamWonMatches = teams[firstTeamIndex].GetWonMatches();
            int firstTeamLostMatches = teams[firstTeamIndex].GetLostMatches();
            int firstTeamDrawMatches = teams[firstTeamIndex].GetDraws();
            int secondTeamWonMatches = teams[secondTeamIndex].GetWonMatches();
            int secondTeamLostMatches = teams[secondTeamIndex].GetLostMatches();
            int secondTeamDrawMatches = teams[secondTeamIndex].GetDraws();

            if (int.TryParse(matchResultArray[0], out int firstTeamScore) && int.TryParse(matchResultArray[1], out int secondTeamScore))
            {
                if (firstTeamScore > secondTeamScore)
                {
                    firstTeamWonMatches++;
                    secondTeamLostMatches++;
                }
                else if (firstTeamScore == secondTeamScore)
                {
                    firstTeamDrawMatches++;
                    secondTeamDrawMatches++;
                }
                else
                {
                    secondTeamWonMatches++;
                    firstTeamLostMatches++;
                }
            }
            else
            {
                return;
            }

            teams.RemoveAt(firstTeamIndex);
            teams.RemoveAt(secondTeamIndex - 1);
            teams.Add(new SoccerTeam(firstTeamName, firstTeamWonMatches, firstTeamLostMatches, firstTeamDrawMatches));
            teams.Add(new SoccerTeam(secondTeamName, secondTeamWonMatches, secondTeamLostMatches, secondTeamDrawMatches));
            teams.Sort();
        }
    }
}
