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

            Assert.Equal(35, ranking.GetTeams()[0].GetPoints());
            Assert.Equal(25, ranking.GetTeams()[1].GetPoints());
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
        public void RankingAddTeamMethodAddsATeamToTheRanking()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            ranking.AddTeam(new SoccerTeam("Pandurii", 1, 3, 9));

            Assert.Equal("Pandurii", ranking.GetTeamOnPosition(4));
        }

        [Fact]
        public void RankingGetTeamOnPositionShouldReturnTheTeamOnTheSpecifiedPosition()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal("FCSB", ranking.GetTeamOnPosition(3));
        }

        [Fact]
        public void RankingGetPositionOfTeamShouldReturnThePositionOfSpecifiedTeam()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(3, ranking.GetPositionOfTeam("FCSB"));
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
            Assert.Equal(11, ranking.GetTeams()[0].GetWonMatches());
            Assert.Equal(8, ranking.GetTeams()[1].GetLostMatches());
        }

        [Fact]
        public void RankingUpdateRankingBasedOnMatchIfNameOfAnyTeamIsNotValidShouldNotMakeAnyChanges()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch("CFR Clj", "2-1", "Uiv. Craiova");
            ranking.UpdateRankingBasedOnMatch("Dinamo Bucuresti", "3-2", "FCSB");

            Assert.Equal(35, ranking.GetTeams()[0].GetPoints());
            Assert.Equal(10, ranking.GetTeams()[0].GetWonMatches());
            Assert.Equal(7, ranking.GetTeams()[1].GetLostMatches());
        }

        [Fact]
        public void RankingUpdateRankingBasedOnMatchIfScoreIsNotValidShouldNotMakeAnyChanges()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch("CFR Cluj", "21", "Univ. Craiova");
            ranking.UpdateRankingBasedOnMatch("CFR Cluj", "=-=", "Univ. Craiova");
            ranking.UpdateRankingBasedOnMatch("CFR Cluj", "1,2", "Univ. Craiova");

            Assert.Equal(35, ranking.GetTeams()[0].GetPoints());
            Assert.Equal(10, ranking.GetTeams()[0].GetWonMatches());
            Assert.Equal(7, ranking.GetTeams()[1].GetLostMatches());
        }
    }
}
