using Xunit;

namespace SoccerRanking
{
    public class SoccerTeamFacts
    {
        [Fact]
        public void SoccerTeamobjectIsInitializedCorrectly()
        {
            const string name = "Pandurii";
            const int wonMatches = 10;
            const int lostMatches = 4;
            const int drawMatches = 5;
            const int points = 35;

            SoccerTeam testTeam = new (name, wonMatches, lostMatches, drawMatches);

            Assert.Equal(name, testTeam.GetName());
            Assert.Equal(points, testTeam.GetPoints());
        }

        [Fact]
        public void GetNameMethodReturnsNameOfTeam()
        {
            const string name = "CFR CLuj";

            SoccerTeam testTeam = new (name, 0, 0, 0);

            Assert.Equal(name, testTeam.GetName());
        }

        [Fact]
        public void GetPointsMethodReturnsPointsOfTeam()
        {
            const string name = "CFR CLuj";
            const int points = 64;

            SoccerTeam testTeam = new (name, 20, 4, 4);

            Assert.Equal(points, testTeam.GetPoints());
        }

        [Fact]
        public void GetMatchesPlayedMethodReturnsTotalMatchesPlayed()
        {
            const int matchesPlayed = 15;

            SoccerTeam testTeam = new ("FCSB", 6, 5, 4);

            Assert.Equal(matchesPlayed, testTeam.GetMatchesPlayed());
        }

        [Fact]
        public void GetWonMatchesMethodReturnsWonMatches()
        {
            SoccerTeam testTeam = new ("FCSB", 6, 5, 4);

            Assert.Equal(6, testTeam.GetWonMatches());
        }

        [Fact]
        public void GetLostMatchesMethodReturnsWonMatches()
        {
            SoccerTeam testTeam = new ("FCSB", 6, 5, 4);

            Assert.Equal(5, testTeam.GetLostMatches());
        }

        [Fact]
        public void GetDrawsMethodReturnsWonMatches()
        {
            SoccerTeam testTeam = new ("FCSB", 6, 5, 4);

            Assert.Equal(4, testTeam.GetDraws());
        }

        [Fact]
        public void CompareToMethodReturnsOneIfFirstTeamIsGreaterThenTheSecondTeam()
        {
            SoccerTeam testTeamOne = new ("FCSB", 6, 5, 4);
            SoccerTeam testTeamTwo = new ("CFR CLuj", 10, 2, 8);
            Assert.Equal(1, testTeamOne.CompareTo(testTeamTwo));
        }

        [Fact]
        public void CompareToMethodReturnsZeroIfFirstTeamIsEqualToTheSecondTeam()
        {
            SoccerTeam testTeamOne = new ("FCSB", 10, 5, 8);
            SoccerTeam testTeamTwo = new ("CFR CLuj", 10, 2, 8);
            Assert.Equal(0, testTeamOne.CompareTo(testTeamTwo));
        }

        [Fact]
        public void CompareToMethodReturnsMinusOneIfFirstTeamIsLesserThenTheSecondTeam()
        {
            SoccerTeam testTeamOne = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamTwo = new ("FCSB", 9, 5, 8);
            Assert.Equal(-1, testTeamOne.CompareTo(testTeamTwo));
        }
    }
}
