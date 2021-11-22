using Xunit;

namespace SoccerRanking
{
    public class SoccerTeamFacts
    {
        [Fact]
        public void SoccerTeamNameAndPointsAreInitializedCorrectly()
        {
            const string name = "Pandurii";
            const int points = 10;

            SoccerTeam testTeam = new (name, points);

            Assert.Equal(testTeam.GetName(), name);
            Assert.Equal(testTeam.GetPoints(), points);
        }

        [Fact]
        public void GetNameMethodReturnsNameOfTeam()
        {
            const string name = "CFR CLuj";

            SoccerTeam testTeam = new (name, 0);

            Assert.Equal(testTeam.GetName(), name);
        }

        [Fact]
        public void GetPointsMethodReturnsPointsOfTeam()
        {
            const string name = "CFR CLuj";
            const int points = 5;

            SoccerTeam testTeam = new (name, points);

            Assert.Equal(testTeam.GetPoints(), points);
        }
    }
}
