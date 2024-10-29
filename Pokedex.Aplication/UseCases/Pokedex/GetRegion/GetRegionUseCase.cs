using Pokedex.Aplication.Integrations.PokeAPI;
using Pokedex.Communication.Response.PokeAPI;
using System.Threading.Tasks;

namespace Pokedex.Aplication.UseCases.Pokedex.GetRegion
{
    public class GetRegionUseCase
    {
        private readonly RegionPokeAPIIntegration _pokeApiIntegration;

        public GetRegionUseCase()
        {
            _pokeApiIntegration = new RegionPokeAPIIntegration();
        }

        public async Task<ResponseRegionPokeAPIListJson> Execute()
        {

            ResponseRegionPokeAPIListJson responselist = await _pokeApiIntegration.GetRegion();

 
            if (responselist != null)
            {
                return responselist; 
            }

            return null; 
        }
    }
}
