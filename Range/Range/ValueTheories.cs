using Xunit;

namespace Range
{
    public class ValueTheories
    {
        [Theory]
        [InlineData("{ }", "")]
        [InlineData("[ ]", "")]
        [InlineData("[ 1 ]", "")]
        [InlineData("[ 1, 2 ]", "")]
        [InlineData("[ \"m\" , \"m\" , \"n\" ]", "")]
        [InlineData("[ true , false , null ]", "")]
        [InlineData("[ false , 2 , null , [ 1 ] ]", "")]
        [InlineData("{ \"telefon\" : 1 }", "")]
        [InlineData("{ \"celular\" : 1 , \"radio\" : 2 }", "")]
        [InlineData("{ \"celular\" : 1 , \"radio\" : { \"frecvente\" : 10 } }", "")]
        [InlineData("{ \"cars\" : { \"Dacia\" : [ {\"model\" : \"Logan\", \"doors\" : 4}], \"Opel\": [ {\"model\":\"Zafira\", \"doors\": 4} ] } }", "")]
        [InlineData("\"Holiday\"", "")]
        [InlineData("1.34E2", "")]
        [InlineData("12.34E2, { \"M\" }", ", { \"M\" }")]
        [InlineData("\"Name\" , [ 1 , 2 , 3 }", " , [ 1 , 2 , 3 }")]
        [InlineData("true", "")]
        [InlineData("false", "")]
        [InlineData("null", "")]
        public void IsValue(string text, string remainingText)
        {
            var value = new Value();

            Assert.True(value.Match(text).Success());
            Assert.Equal(remainingText, value.Match(text).RemainingText());
        }

        [Theory]
        [InlineData("{ ]", "{ ]")]
        [InlineData("[ }", "[ }")]
        [InlineData("[ \"cars\" : 1 ]", "[ \"cars\" : 1 ]")]
        [InlineData("[ \"cars\" : 1 , \"phones\" : 2 ]", "[ \"cars\" : 1 , \"phones\" : 2 ]")]
        [InlineData("{ true , false , null }", "{ true , false , null }")]
        [InlineData("[ false , 2 , null , [ 1 } ]", "[ false , 2 , null , [ 1 } ]")]
        [InlineData("{ \"celular\" : 1 , \"radio\" : { \"frecvente\" : 10 ] }", "{ \"celular\" : 1 , \"radio\" : { \"frecvente\" : 10 ] }")]
        public void IsNotValue(string text, string remainingText)
        {
            var value = new Value();

            Assert.False(value.Match(text).Success());
            Assert.Equal(remainingText, value.Match(text).RemainingText());
        }
    }
}
