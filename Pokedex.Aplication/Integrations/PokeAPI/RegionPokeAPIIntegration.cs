using Newtonsoft.Json;
using Pokedex.Communication.Response.PokeAPI;
using System.Text.Json;

namespace Pokedex.Aplication.Integrations.PokeAPI
{
    internal class RegionPokeAPIIntegration
    {
        public async Task<ResponseRegionPokeAPIListJson> GetRegion()
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/region");

                if (!response.IsSuccessStatusCode)
                {
                    return null; 
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<ResponseRegionPokeAPIListJson>(jsonString);
                return jsonObject;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseLocationsListPokeAPIJson> GetLocations(string name)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/region/{name}");

                if (!response.IsSuccessStatusCode)
                {
                    return null; 
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<ResponseLocationsListPokeAPIJson>(jsonString);
                return locations;
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<ResponsePokemonEncountersListJson>> GetAllAreaPokemon(ResponseLocationsListPokeAPIJson data, string name)
        {
            HttpClient httpClient = new HttpClient();
            List<ResponsePokemonEncountersListJson> encounterResponses = new List<ResponsePokemonEncountersListJson>();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            for (var i = 0; i < data.Locations.Count; i++)
            {
                var area = data.Locations[i].Url;
                var response = await httpClient.GetAsync(area);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var areasData = System.Text.Json.JsonSerializer.Deserialize<ResponseAreaListJson>(jsonString, options);

                    if (areasData != null)
                    {
                        for (var index = 0; index < areasData.Areas.Count; index++)
                        {
                            var AreasResponse = await httpClient.GetAsync(areasData.Areas[index].Url);

                            if (AreasResponse.IsSuccessStatusCode)
                            {
                                var jsonStringArea = await AreasResponse.Content.ReadAsStringAsync();
                                var encounterData = System.Text.Json.JsonSerializer.Deserialize<ResponsePokemonEncountersListJson>(jsonStringArea, options);

                                if (encounterData != null)
                                {
                                    encounterData.Name = name; 
                                    encounterResponses.Add(encounterData);
                                }
                            }
                        }
                    }
                }
            }

            return encounterResponses;
        }


        public async Task<ResponseProfilePokemonJson> GetPokemon(string name)
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                var pokemon = JsonConvert.DeserializeObject<ResponsePokemonSingleJson>(jsonString);
                if (pokemon != null)
                {
                    var responseAll = await this.GetTypePokemon(pokemon);
                    return responseAll;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseProfilePokemonJson> GetTypePokemon(ResponsePokemonSingleJson pokemon)
        {
            if (pokemon.Types != null && pokemon.Types.Count > 0 && pokemon.Types[0].Type != null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string type = pokemon.Types[0].Type.Name;
                    var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/type/{type}");
                    response.EnsureSuccessStatusCode(); 
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var typeDetails = JsonConvert.DeserializeObject<ResponseTypePokemonAllJson>(jsonString);

                    return new ResponseProfilePokemonJson
                    {
                        Profile = pokemon,
                        Type = typeDetails
                    };
                }
            }

            return null; 
        }
    }
}
