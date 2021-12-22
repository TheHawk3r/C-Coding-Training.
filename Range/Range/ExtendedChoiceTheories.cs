using Xunit;

namespace Range
{
    public class ExtendedChoiceTheories
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
        public void CanAddPatternToValueChoicePatternsAndAddedPatternsAreValidAndConsumed(string text, string remainingText)
        {
            var jsonString = new JsonString();
            var number = new Number();
            var ws = new Choice(new Any("\u0020\u000A\u000D\u0009"), new Text(""));
            var value = new Choice(
                jsonString,
                number,
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var element = new Sequence(ws, value, ws);
            var elements = new List(element, new Character(','));

            var member = new Sequence(ws, jsonString, ws, new Character(':'), element);
            var members = new List(member, new Character(','));

            var array = new Choice(
                new Sequence(new Character('['), ws, new Character(']')),
                new Sequence(new Character('['), elements, new Character(']')));

            var jsonObject = new Choice(
                new Sequence(new Character('{'), ws, new Character('}')),
                new Sequence(new Character('{'), members, new Character('}')));

            value.Add(array);
            value.Add(jsonObject);

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

        public void TextIsNotObjectOrArrayPattern(string text, string remainingText)
        {
            var jsonString = new JsonString();
            var number = new Number();
            var ws = new Choice(new Any("\u0020\u000A\u000D\u0009"), new Text(""));
            var value = new Choice(
                jsonString,
                number,
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var element = new Sequence(ws, value, ws);
            var elements = new List(element, new Character(','));

            var member = new Sequence(ws, jsonString, ws, new Character(':'), element);
            var members = new List(member, new Character(','));

            var array = new Choice(
                new Sequence(new Character('['), ws, new Character(']')),
                new Sequence(new Character('['), elements, new Character(']')));

            var jsonObject = new Choice(
                new Sequence(new Character('{'), ws, new Character('}')),
                new Sequence(new Character('{'), members, new Character('}')));

            value.Add(array);
            value.Add(jsonObject);

            Assert.False(value.Match(text).Success());
            Assert.Equal(remainingText, value.Match(text).RemainingText());
        }
    }
}
