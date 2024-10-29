namespace Pokedex.Communication.Response.PokeAPI
{
    public class ResponsePokemonEncountersListJson
    {
        public string Name { get; set; } = string.Empty;
        public List<ResponsePokemonAreaPokeALLAPIJson> Pokemon_encounters { get; set; } = new List<ResponsePokemonAreaPokeALLAPIJson>();
    }
}
