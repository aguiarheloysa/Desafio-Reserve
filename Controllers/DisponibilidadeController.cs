using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Teste1.DTOs;
using Teste1.Service;

namespace Teste1.Controllers
{
    [ApiController]
    [Route("disponibilidade")]
    public class DisponibilidadeController : ControllerBase
    {
        private readonly IFlightSearchService _flightSearchService;

        public DisponibilidadeController(IFlightSearchService flightSearchService)
        {
            _flightSearchService = flightSearchService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightResponse>>> Get(
            [FromQuery] string origem, 
            [FromQuery] string destino, 
            [FromQuery] string data)
        {
            if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino) || string.IsNullOrEmpty(data))
            {
                return BadRequest("Origem, destino e data são obrigatórios.");
            }

            var result = FlightSearchService(origem, destino, data);

            return Ok(result);
        }

        // Simula o sistema da GOL com seu próprio formato
        private async Task<IEnumerable<GOLResponse>> GetGolFlights(string origem, string destino, string data)
        {
            HttpClient httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.GetAsync("www.gol.com");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return new List<GOLResponse>
            {
                new GOLResponse { Codigo = "1234", De = origem, Para = destino, Decolagem = DateTime.Parse(data + " 10:00:00"), Pouso = DateTime.Parse(data + " 11:00:00"), Valor = 299.99m },
                new GOLResponse { Codigo = "4321", De = origem, Para = destino, Decolagem = DateTime.Parse(data + " 14:00:00"), Pouso = DateTime.Parse(data + " 15:00:00"), Valor = 150.00m }
            };
        }

        // Simula o sistema da LATAM com seu próprio formato
        private IEnumerable<LATAMResponse> GetLatamFlights(string origem, string destino, string data)
        {
            return new List<LATAMResponse>
            {
                new LATAMResponse { FlightNumber = "5678", OriginCity = origem, DestinationCity = destino, DepartureTime = data + " 09:00:00", ArrivalTime = data + " 09:50:00", Price = 350.70m },
                new LATAMResponse { FlightNumber = "8765", OriginCity = origem, DestinationCity = destino, DepartureTime = data + " 20:00:00", ArrivalTime = data + " 21:00:00", Price = 250.00m }
            };
        }
    }
}
