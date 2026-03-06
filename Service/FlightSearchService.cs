using Teste1.DTOs;

namespace Teste1.Service

{
    public interface IFlightSearchService

    {
        Task<IEnumerable<FlightResponse>> SearchFlightsAsync(string origin, string destination, string date);
    }

    public class FlightSearchService : IFlightSearchService

    {
        public async Task<IEnumerable<FlightResponse>> SearchFlightsAsync(string origin, string destination, string date)
        {

            // Simulando a busca em diferentes cias aéreas com diferentes formatos

            var golFlights = await GetGolFlights(origem, destino, data);
            var latamFlights = await GetLatamFlights(origem, destino, data);

            // Agregação e conversão para o formato padrão do portal
            var allFlights = golFlights.Select(f => new FlightResponse
            {

                Voo = f.Codigo,
                Companhia = "GOL",
                Origem = f.De,
                Destino = f.Para,
                Partida = f.Decolagem.ToString("yyyy-MM-dd HH:mm:ss"),
                Chegada = f.Pouso.ToString("yyyy-MM-dd HH:mm:ss"),
                Tarifa = f.Valor

            }).Concat(latamFlights.Select(f => new FlightResponse
                {
                    Voo = f.FlightNumber,
                    Companhia = "LATAM",
                    Origem = f.OriginCity,
                    Destino = f.DestinationCity,
                    Partida = DateTime.Parse(f.DepartureTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    Chegada = DateTime.Parse(f.ArrivalTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    Tarifa = f.Price

                }));



            // Requisito de Negócio: Ordenação por menor tarifa e depois por horário de partida

            var result = allFlights.OrderBy(f => f.Tarifa)
                .ThenBy(f => f.Partida)
                .ToList();

            return results.SelectMany(r => r)
                .OrderBy(f => f.Tarifa)
                .ThenBy(f => f.Partida)
                .ToList();

        }

    }

}

