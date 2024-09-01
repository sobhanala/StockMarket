namespace api.ApplicationService.Dtos.Stock
{
    public class CreateStockDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; } = 0;
        public decimal LastDiv { get; set; } = 0;
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } = 0;

        public DataAccess.Model.Stock ToStockModel() 
        {
            return new DataAccess.Model.Stock
            {
                Symbol = this.Symbol,
                CompanyName = this.CompanyName,
                Purchase = this.Purchase,
                LastDiv = this.LastDiv,
                Industry = this.Industry,
                MarketCap = this.MarketCap
            };
        } 
    }
}