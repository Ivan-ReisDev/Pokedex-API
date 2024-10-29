using Microsoft.AspNetCore.Mvc;
using Pokedex.Aplication.UseCases.Pokedex.GetAllPokemonsRegion;
using Pokedex.Aplication.UseCases.Pokedex.GetPokemonSingle;
using Pokedex.Aplication.UseCases.Pokedex.GetRegion;
using Pokedex.Communication.Response;
using Pokedex.Communication.Response.PokeAPI;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Pokedex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokedexController : ControllerBase
    {
   
            private readonly GetRegionUseCase _getRegionUseCase;
            public PokedexController(GetRegionUseCase getRegionUseCase)
            {
                _getRegionUseCase = getRegionUseCase;
            }

        [HttpGet]
        [Route("region")]
        [ProducesResponseType(typeof(ResponseRegionPokeAPIListJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegion()
        {
            var response = await _getRegionUseCase.Execute();
            if (response != null)
            {
                return Ok(response);
            }
            var error = new ResponseErrosJson("Região não encontrada.");
            return NotFound();
         }


        [HttpGet]
        [Route("pokemons/{location}")]
        [ProducesResponseType(typeof(ResponsePokemonEncountersListJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPokemons([FromRoute] string location)
        {
            var useCases = new GetAllPokemonsRegionUseCases();
            var response = await useCases.Execute(location);

            if (response != null)
            {
                return Ok(response);
            }
            var error = new ResponseErrosJson("Nenhum pokemon encontrado nessa região");
            return NotFound(error);
        }

        [HttpGet]
        [Route("pokemon/{namePokemon}")]
        [ProducesResponseType(typeof(ResponseProfilePokemonJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetPokemon([FromRoute] string namePokemon)
        {
            var useCases = new GetPokemonSingleUseCase();
            var response = await useCases.Execute(namePokemon);

            if (response != null)
            {
                return Ok(response);
            }
            var error = new ResponseErrosJson("Pokémon não encontrado");
            return NotFound(error);

            
        }






    }


    
}
