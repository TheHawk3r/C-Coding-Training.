using Xunit;

namespace Range
{
    public class ExtendedChoiceFacts
    {
        [Fact]
        public void CanAddPatternToValueChoicePatterns()
        {
            var jsonString = new JsonString();
            var number = new Number();
            var ws = new Choice(new Text(""), new Any("\u0020\u000A\u000D\u0009"));
            var value = new Choice(
                jsonString,
                number,
                new Text("true"),
                new Text("false"),
                new Text("null"));

            var element = new Sequence(ws, value, ws);
            var elements = new Choice(
                element,
                new List(element, new Character(',')));

            var member = new Sequence(ws, jsonString, ws, new Character(':'), element);
            var members = new Choice(
                member,
                new List(member, new Character(',')));

            var array = new Choice(
                new Sequence(new Character('['), ws, new Character(']')),
                new Sequence(new Character('{'), elements, new Character('}')));

            var jsonObject = new Choice(
                new Sequence(new Character('['), ws, new Character(']')),
                new Sequence(new Character('{'), members, new Character('}')));

            var expectedValue = new Choice(
                jsonObject,
                array,
                jsonString,
                number,
                new Text("true"),
                new Text("false"),
                new Text("null"));

            value.Add(array);
            value.Add(jsonObject);

            Assert.Equal(expectedValue, value);
        }
    }
}
