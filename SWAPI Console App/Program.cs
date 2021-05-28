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
            /*var SelectedPlanet = await ProcessPlanet(1);

            Console.WriteLine(SelectedPlanet.Name + "\t" + SelectedPlanet.Climate);

            var AllPlanets = await ProcessPlanets();
            foreach(var Planet in AllPlanets)
            {
                Console.WriteLine(Planet);
            }*/
            Console.Write("Input a planet ID, otherwise leave empty to show all planets. \t");
            var PlanetID = Console.ReadLine();
            int result;
            if (int.TryParse(PlanetID, out result) && result > 0 && result < 61 )
            {
                var SelectedPlanet = await ProcessPlanet(int.Parse(PlanetID));
                Console.WriteLine(SelectedPlanet.Name + "\t" + SelectedPlanet.Climate);
            } else if (PlanetID == "")
            {
                var AllPlanets = await ProcessPlanets();
                foreach (var Planet in AllPlanets)
                {
                    Console.WriteLine(Planet);
                }
            } else
            {
                Console.WriteLine("Not a valid input.");
            }
        }

        private static async Task<Planet> ProcessPlanet(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = client.GetStreamAsync("https://swapi.dev/api/planets/" + id.ToString() + "/");
            var SelectedPlanet = await JsonSerializer.DeserializeAsync<Planet>(await streamTask);
            return SelectedPlanet;
        }

        private static async Task<List<string>> ProcessPlanets()
        {
            var AllPlanets = new List<string>();
            client.DefaultRequestHeaders.Accept.Clear();
            for (var i = 1; i < 7; i++)
            {
                var Page = client.GetStreamAsync("https://swapi.dev/api/planets/?page=" + i);
                var Results = JsonSerializer.DeserializeAsync<Query>(await Page);
                foreach(var thing in Results.Result.Results)
                {
                    AllPlanets.Add(thing.Name + "\t" + thing.Climate);
                }
            }
            return AllPlanets;
        }
    }
}
