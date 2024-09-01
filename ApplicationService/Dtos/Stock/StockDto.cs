namespace api.ApplicationService.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; } 
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } 

        public StockDto(DataAccess.Model.Stock stock) 
        {
            this.Id = stock.Id;
            this.Symbol = stock.Symbol;
            this.CompanyName = stock.CompanyName;
            this.Purchase = stock.Purchase;
            this.LastDiv = stock.LastDiv;
            this.Industry = stock.Industry;
            this.MarketCap = stock.MarketCap;
        }
    }
}