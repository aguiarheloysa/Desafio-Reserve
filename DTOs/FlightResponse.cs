using System.Text.Json.Serialization;

namespace Teste1.DTOs
{
    public class FlightResponse
    {
        [JsonPropertyName("voo")]
        public required string Voo { get; set; }

        [JsonPropertyName("companhia")]
        public required string Companhia { get; set; }

        [JsonPropertyName("origem")]
        public required string Origem { get; set; }

        [JsonPropertyName("destino")]
        public required string Destino { get; set; }

        [JsonPropertyName("partida")]
        public required string Partida { get; set; }

        [JsonPropertyName("chegada")]
        public required string Chegada { get; set; }

        [JsonPropertyName("tarifa")]
        public decimal Tarifa { get; set; }
    }
}
