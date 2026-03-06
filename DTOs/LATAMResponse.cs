namespace Teste1.DTOs
{
    public class LATAMResponse
    {
        public required string FlightNumber { get; set; }
        public required string OriginCity { get; set; }
        public required string DestinationCity { get; set; }
        public required string DepartureTime { get; set; }
        public required string ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
