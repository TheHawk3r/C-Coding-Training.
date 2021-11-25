using System.Collections.Generic;
using Xunit;

namespace SoccerRanking
{
    public class RankingFacts
    {
        [Fact]
        public void RankingAfterInstantiationSortsItself()
        {
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));

            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(cfrCluj, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiova, ranking.GetTeamOnPosition(2));
        }

        [Fact]
        public void AddTeamMethodAddsATeamToTheRanking()
        {
            SoccerTeam pandurii = new SoccerTeam("Pandurii", 1, 3, 9);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            ranking.AddTeam(new SoccerTeam("Pandurii", 1, 3, 9));

            Assert.Equal(pandurii, ranking.GetTeamOnPosition(4));
        }

        [Fact]
        public void GetTeamOnPositionShouldReturnTheTeamOnTheSpecifiedPosition()
        {
            SoccerTeam fcsb = new SoccerTeam("FCSB", 7, 4, 2);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(fcsb, ranking.GetTeamOnPosition(3));
        }

        [Fact]
        public void GetPositionOfTeamShouldReturnThePositionOfSpecifiedTeam()
        {
            SoccerTeam fcsb = new SoccerTeam("FCSB", 7, 4, 2);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(new SoccerTeam("CFR Cluj", 10, 4, 5));
            soccerTeams.Add(new SoccerTeam("FCSB", 7, 4, 2));
            soccerTeams.Add(new SoccerTeam("Univ. Craiova", 5, 7, 10));
            Ranking ranking = new Ranking(soccerTeams);

            Assert.Equal(3, ranking.GetPositionOfTeam(fcsb));
        }

        [Fact]
        public void UpdaterankingBasedOnMatchMethodUpdatesRankingCorrectlyAfterAMatchInWitchTheFirstTeamWins()
        {
            SoccerTeam fcsb = new SoccerTeam("FCSB", 7, 4, 2);
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            SoccerTeam cfrClujAfterUpdate = new SoccerTeam("CFR Cluj", 11, 4, 5);
            SoccerTeam univCraiovaAfterUpdate = new SoccerTeam("Univ. Craiova", 5, 8, 10);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(cfrCluj);
            soccerTeams.Add(fcsb);
            soccerTeams.Add(univCraiova);

            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch(cfrCluj, "2-1", univCraiova);

            Assert.Equal(cfrClujAfterUpdate, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiovaAfterUpdate, ranking.GetTeamOnPosition(2));
        }

        [Fact]
        public void UpdaterankingBasedOnMatchMethodUpdatesRankingCorrectlyAfterAMatchInWitchTheSecondTeamWins()
        {
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            SoccerTeam cfrClujAfterUpdate = new SoccerTeam("CFR Cluj", 10, 5, 5);
            SoccerTeam univCraiovaAfterUpdate = new SoccerTeam("Univ. Craiova", 6, 7, 10);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(cfrCluj);
            soccerTeams.Add(univCraiova);

            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch(cfrCluj, "1-3", univCraiova);

            Assert.Equal(cfrClujAfterUpdate, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiovaAfterUpdate, ranking.GetTeamOnPosition(2));
        }

        [Fact]
        public void UpdaterankingBasedOnMatchMethodUpdatesRankingCorrectlyAfterAMatchThatEndsInADraw()
        {
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            SoccerTeam cfrClujAfterUpdate = new SoccerTeam("CFR Cluj", 10, 4, 6);
            SoccerTeam univCraiovaAfterUpdate = new SoccerTeam("Univ. Craiova", 5, 7, 11);
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            soccerTeams.Add(cfrCluj);
            soccerTeams.Add(univCraiova);

            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch(cfrCluj, "1-1", univCraiova);

            Assert.Equal(cfrClujAfterUpdate, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiovaAfterUpdate, ranking.GetTeamOnPosition(2));
        }

        [Fact]
        public void UpdateRankingBasedOnMatchIfAnyTeamIsNotValidShouldNotMakeAnyChanges()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            SoccerTeam cfrClujWithWrongData = new SoccerTeam("CFR Cluj", 9, 4, 5);
            SoccerTeam univCraiovaWithWrongData = new SoccerTeam("Univ. Craiova", 5, 6, 10);
            SoccerTeam dinamoBucuresti = new SoccerTeam("Univ. Craiova", 3, 8, 10);
            SoccerTeam fcsb = new SoccerTeam("FCSB", 7, 4, 2);
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            soccerTeams.Add(cfrCluj);
            soccerTeams.Add(fcsb);
            soccerTeams.Add(univCraiova);
            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch(cfrClujWithWrongData, "2-1", univCraiovaWithWrongData);
            ranking.UpdateRankingBasedOnMatch(dinamoBucuresti, "3-2", fcsb);

            Assert.Equal(cfrCluj, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiova, ranking.GetTeamOnPosition(2));
        }

        [Fact]
        public void UpdateRankingBasedOnMatchIfScoreIsNotValidShouldNotMakeAnyChanges()
        {
            List<SoccerTeam> soccerTeams = new List<SoccerTeam>();
            SoccerTeam cfrCluj = new SoccerTeam("CFR Cluj", 10, 4, 5);
            SoccerTeam univCraiova = new SoccerTeam("Univ. Craiova", 5, 7, 10);
            SoccerTeam fcsb = new SoccerTeam("FCSB", 7, 4, 2);
            soccerTeams.Add(cfrCluj);
            soccerTeams.Add(fcsb);
            soccerTeams.Add(univCraiova);
            Ranking ranking = new Ranking(soccerTeams);

            ranking.UpdateRankingBasedOnMatch(cfrCluj, "21", univCraiova);
            ranking.UpdateRankingBasedOnMatch(cfrCluj, "=-=", univCraiova);
            ranking.UpdateRankingBasedOnMatch(cfrCluj, "1,2", univCraiova);

            Assert.Equal(cfrCluj, ranking.GetTeamOnPosition(1));
            Assert.Equal(univCraiova, ranking.GetTeamOnPosition(2));
        }
    }
}
