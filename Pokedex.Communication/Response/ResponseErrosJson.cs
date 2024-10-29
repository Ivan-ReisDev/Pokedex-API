namespace Pokedex.Communication.Response
{
    public class ResponseErrosJson
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ResponseErrosJson() { }

        public ResponseErrosJson(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public ResponseErrosJson(IEnumerable<string> errorMessages)
        {
            Errors.AddRange(errorMessages);
        }
    }
}