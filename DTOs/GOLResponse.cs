namespace Teste1.DTOs
{
    public class GOLResponse
    {
        public required string Codigo { get; set; }
        public required string De { get; set; }
        public required string Para { get; set; }
        public DateTime Decolagem { get; set; }
        public DateTime Pouso { get; set; }
        public decimal Valor { get; set; }
    }
}
