namespace Range
{
    public class Value : IPattern
    {
        private readonly IPattern pattern;

        public Value()
        {
            var jsonString = new JsonString();
            var number = new Number();
            var ws = new Many(new Any("\n\r\t "));
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

            var array = new Sequence(new Character('['), elements, ws, new Character(']'));

            var jsonObject = new Sequence(new Character('{'), members, ws, new Character('}'));

            value.Add(jsonObject);
            value.Add(array);

            pattern = element;
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
