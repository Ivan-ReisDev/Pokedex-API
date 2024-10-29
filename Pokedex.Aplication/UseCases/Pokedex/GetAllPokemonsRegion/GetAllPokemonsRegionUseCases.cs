using Pokedex.Aplication.Integrations.PokeAPI;
using Pokedex.Communication.Response.PokeAPI;

namespace Pokedex.Aplication.UseCases.Pokedex.GetAllPokemonsRegion
{
    public class GetAllPokemonsRegionUseCases
    {
        private readonly RegionPokeAPIIntegration _pokeApiIntegration;

        public GetAllPokemonsRegionUseCases()
        {
            _pokeApiIntegration = new RegionPokeAPIIntegration();
        }
        public async Task<List<ResponsePokemonEncountersListJson>> Execute(string name)
        {
            string nameFormat = name.ToLower().Replace(" ", "");
            ResponseLocationsListPokeAPIJson responselist = await _pokeApiIntegration.GetLocations(nameFormat);


            if (responselist != null)
            {
                List<ResponsePokemonEncountersListJson> responsePokemons = await _pokeApiIntegration.GetAllAreaPokemon(responselist, nameFormat);
                return responsePokemons;
            }

            return null;
        }

    }
}
