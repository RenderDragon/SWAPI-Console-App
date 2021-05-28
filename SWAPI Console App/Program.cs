/*
 *    Console app that returns a list of planets from the Star Wars films
 *    Takes in an optional argument for planet id
 *    
 *    Author: Sylvester Soetianto
 *    
 *    1.    Create and initialise HttpClient
 *    2.    Send a GET request to the SWAPI endpoint, w/ optional argument for planet ID
 *    3.    Analyse the response for JSON
 *    4.    Deserialize JSON for planet name and climate (expected: 60 results over 6 pages)
 *    5.    Store planets as an array
 *    6.    Output planet name + climate to console
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWAPI_Console_App
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            var SelectedPlanet = await ProcessPlanets();

            Console.WriteLine(SelectedPlanet.Name);
            Console.WriteLine(SelectedPlanet.Climate);
        }

        private static async Task<Planet> ProcessPlanets()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = client.GetStreamAsync("https://swapi.dev/api/planets/1/");
            var SelectedPlanet = await JsonSerializer.DeserializeAsync<Planet>(await streamTask);
            return SelectedPlanet;
        }
    }
}
