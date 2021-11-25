using Xunit;

namespace SoccerRanking
{
    public class SoccerTeamFacts
    {
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

        [Fact]
        public void EqualsMethodChecksIfTwoSoccerTeamObjectsAreEqualUsingMemberFields()
        {
            SoccerTeam testTeamOne = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamTwo = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamThree = new ("FCSB", 10, 2, 8);
            Assert.True(testTeamOne.Equals(testTeamTwo));
            Assert.False(testTeamTwo.Equals(testTeamThree));
        }

        [Fact]
        public void AddWonMatchMethodShouldIncrementMatchesWonFieldAndUpdatePointsField()
        {
            SoccerTeam testTeam = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamAfterWonMatchAdded = new ("CFR CLuj", 11, 2, 8);

            testTeam.AddWonMatch();

            Assert.Equal(testTeam, testTeamAfterWonMatchAdded);
        }

        [Fact]
        public void AddLostMatchMethodShouldIncrementMatchesLostField()
        {
            SoccerTeam testTeam = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamAfterLostMatchAdded = new ("CFR CLuj", 10, 3, 8);

            testTeam.AddLostMatch();

            Assert.Equal(testTeam, testTeamAfterLostMatchAdded);
        }

        [Fact]
        public void AddDrawMethodShouldIncrementDrawMaatchesFieldAndUpdatePointsField()
        {
            SoccerTeam testTeam = new ("CFR CLuj", 10, 2, 8);
            SoccerTeam testTeamAfterLostMatchAdded = new ("CFR CLuj", 10, 2, 9);

            testTeam.AddDraw();

            Assert.Equal(testTeam, testTeamAfterLostMatchAdded);
        }
    }
}
