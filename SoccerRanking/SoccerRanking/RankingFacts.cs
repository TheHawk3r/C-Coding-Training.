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
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10));
            soccerTeams.Add(new SoccerTeam("FCSB", 8));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 11));

            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(11, soccerTeams[0].GetPoints());
            Assert.Equal(10, soccerTeams[1].GetPoints());
        }
    }
}
