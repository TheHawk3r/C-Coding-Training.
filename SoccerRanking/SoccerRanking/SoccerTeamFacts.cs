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
    }
}
