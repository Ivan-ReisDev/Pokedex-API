namespace Pokedex.Communication.Response.PokeAPI
{
    public class ResponsePokemonSingleJson
    {
        public List<AbilityInfo> Abilities { get; set; }
        public List<TypeInfo> Types { get; set; }
        public List<FormInfo> Forms { get; set; }
        public List<StatInfo> Stats { get; set; } 
    }

    public class AbilityInfo
    {
        public Ability Ability { get; set; }
        public bool IsHidden { get; set; }
    }

    public class Ability
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class TypeInfo
    {
        public int Slot { get; set; }
        public Type Type { get; set; }
    }

    public class Type
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class FormInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class StatInfo
    {
        public int Base_stat { get; set; }
        public int Effort { get; set; }
        public Stat Stat { get; set; } 
    }

    public class Stat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
