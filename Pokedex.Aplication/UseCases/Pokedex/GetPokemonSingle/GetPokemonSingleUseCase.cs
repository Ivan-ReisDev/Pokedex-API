using Pokedex.Aplication.Integrations.PokeAPI;
using Pokedex.Communication.Response.PokeAPI;

namespace Pokedex.Aplication.UseCases.Pokedex.GetPokemonSingle
{
    public class GetPokemonSingleUseCase
    {

        private readonly RegionPokeAPIIntegration _pokeApiIntegration;

        public GetPokemonSingleUseCase()
        {
            _pokeApiIntegration = new RegionPokeAPIIntegration();
        }

        public async Task<ResponseProfilePokemonJson> Execute(string name)
        {
            string nameFormat = name.ToLower().Replace(" ", "");
            ResponseProfilePokemonJson response = await _pokeApiIntegration.GetPokemon(nameFormat);
            if (response != null)
            {
                return response;
            }

            return null; 
        }

    }
}
