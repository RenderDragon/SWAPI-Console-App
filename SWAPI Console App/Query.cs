using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SWAPI_Console_App
{
    class Query
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }
        
        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("results")]
        public List<Planet> Results { get; set; }

        public class Enumerator
        {
            public Query Current { get; private set; }
            public bool MoveNext()
            {
                if (this.Current == null)
                {
                    this.Current = new Query();
                    return true;
                }
                this.Current = null;
                return false;
            }
            public void Reset() { this.Current = null; }
        }
        public Enumerator GetEnumerator() { return new Enumerator(); }
    }
}
