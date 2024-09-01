namespace api.ApplicationService.Dtos.Stock
{
    public class UpdateStockDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; } = 0;
        public decimal LastDiv { get; set; } = 0;
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } = 0;

        public DataAccess.Model.Stock ToStockModel(int id)
        {
            return new DataAccess.Model.Stock()
            {
                Id = id,
                Symbol = this.Symbol,
                CompanyName = this.CompanyName,
                Purchase = this.Purchase,
                LastDiv = this.LastDiv,
                Industry = this.Industry,
                MarketCap = this.MarketCap,
            };
        }
    }
}