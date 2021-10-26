using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PokemonWebApp.Models;
using System.Net.Http.Headers;
using System.Net.Http;

namespace PokemonWebApp.Pages.Pokemons

{
    public class IndexModel : PageModel
    {
        public List<Pokemon> Pokemons { get; private set; } = new List<Pokemon>();
        string baseURL = "https://localhost:44395/";
        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Pokemons");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(result);
                }
            }

        }
    }
}
