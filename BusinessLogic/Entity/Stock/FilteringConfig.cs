namespace api.BusinessLogic.Entity.Stock
{
    public class FilteringConfig
    {
        public string SymbolContains { get; set; } = string.Empty;
        public string CompanyNameContains { get; set; } = string.Empty;
        public decimal? PurchaseLowerBound { get; set; } = null;
        public decimal? PurchaseUpperBound { get; set; } = null;
        public decimal? LastDivLowerBound { get; set; } = null;
        public decimal? LastDivUpperBound { get; set; } = null;
        public string IndustryContains { get; set; } = string.Empty;
        public decimal? MarketCapLowerBound { get; set; } = null;
        public decimal? MarketCapUpperBound { get; set; } = null;
    }
}
