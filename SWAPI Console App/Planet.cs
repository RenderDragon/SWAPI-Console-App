/*
 *  JSON deserialisation of Planets from the SWAPI endpoint
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SWAPI_Console_App
{
    class Planet
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("climate")]
        public string Climate { get; set; }
    }
}
