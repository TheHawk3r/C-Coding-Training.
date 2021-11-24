using System.Collections.Generic;
using Xunit;

namespace SoccerRanking
{
    public class RankingFacts
    {
        [Fact]
        public void RankingAfterInstantiationSortsItself()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));

            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(35, soccerTeams[0].GetPoints());
            Assert.Equal(25, soccerTeams[1].GetPoints());
        }

        [Fact]
        public void RankingGetTeamsMethodReturnsTeamsInRanking()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(soccerTeams, ranking.GetTeams());
        }

        [Fact]
        public void RankingUpdatesCorrectlyAfterMatch()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch("CFR Cluj", "2-1", "Univ. Craiova");

            Assert.Equal(38, ranking.GetTeams()[0].GetPoints());
        }
    }
}
