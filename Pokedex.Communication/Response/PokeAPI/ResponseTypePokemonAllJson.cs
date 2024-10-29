namespace Pokedex.Communication.Response.PokeAPI
{
    public class ResponseTypePokemonAllJson
    {
        public DamageRelations damage_relations { get; set; }
    }

    public class DamageRelations
    {
        public List<TypeDetails> double_damage_from { get; set; }
        public List<TypeDetails> double_damage_to { get; set; }
    }

    public class TypeDetails
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
