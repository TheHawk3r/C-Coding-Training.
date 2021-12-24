namespace Range
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
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

            value.Add(jsonObject);
            value.Add(array);

            pattern = value;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
