using System.Linq.Expressions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace api.BusinessLogic.Entity.Stock
{
    public class StockQueryObject
    {
        public FilteringConfig? FilteringConfig { get; set; } = null;
        public SortingConfig? SortingConfig { get; set; } = null;
        public bool EnablePagination { get; set; } = false;
        public int PageSize { get; set; } = 0;
        public int PageNumber { get; set; } = 1;


        public IQueryable<DataAccess.Model.Stock> ApplyAllQueries(IQueryable<DataAccess.Model.Stock> stocks)
        {
            var targetStocks = ApplyFiltering(stocks);
            targetStocks = ApplySorting(targetStocks);
            targetStocks = ApplyPagination(targetStocks);
            return targetStocks;
        }

        public IQueryable<DataAccess.Model.Stock> ApplyFiltering(IQueryable<DataAccess.Model.Stock> stocks)
        {
            IQueryable<DataAccess.Model.Stock> result = stocks;
            FilteringConfig? conf = FilteringConfig;
            if (conf is null)
            {
                return result;
            }
            if (conf.SymbolContains != string.Empty)
            {
                result = result.Where(s => s.Symbol.Contains(conf.SymbolContains));
            }
            if (conf.CompanyNameContains != string.Empty)
            {
                result = result.Where(s => s.CompanyName.Contains(conf.CompanyNameContains));
            }
            if (conf.PurchaseLowerBound != null) 
            {
                result = result.Where(s => s.Purchase >= conf.PurchaseLowerBound);
            }
            if (conf.PurchaseUpperBound != null) 
            {
                result = result.Where(s => s.Purchase <= conf.PurchaseUpperBound);
            }
            if (conf.LastDivLowerBound != null)
            {
                result = result.Where(s => s.LastDiv >= conf.LastDivLowerBound);
            }
            if (conf.LastDivUpperBound != null)
            {
                result = result.Where(s => s.LastDiv <= conf.LastDivUpperBound);
            }
            if (conf.IndustryContains != string.Empty)
            {
                result = result.Where(s => s.Industry.Contains(conf.IndustryContains));
            }
            if (conf.MarketCapLowerBound != null)
            {
                result = result.Where(s => s.MarketCap >= conf.MarketCapLowerBound);
            }
            if (conf.MarketCapUpperBound != null)
            {
                result = result.Where(s => s.MarketCap <= conf.MarketCapUpperBound);
            }
            return result;
        } 

        public IQueryable<DataAccess.Model.Stock> ApplySorting(IQueryable<DataAccess.Model.Stock> stocks)
        {
            IQueryable<DataAccess.Model.Stock> result = stocks;
            SortingConfig? conf = SortingConfig;
            if (conf == null || conf.SortingField == null) return result;

            switch(conf.SortingField)
            {
                case SortingField.Id: result = result.OrderBy(s => s.Id); break;
                case SortingField.Symbol: result = result.OrderBy(s => s.Symbol); break;
                case SortingField.CompanyName: result = result.OrderBy(s => s.CompanyName); break;
                case SortingField.Purchase: result = result.OrderBy(s => s.Purchase); break;
                case SortingField.LastDiv: result = result.OrderBy(s => s.LastDiv); break;
                case SortingField.Industry: result = result.OrderBy(s => s.Industry); break;
                case SortingField.MarketCap: result = result.OrderBy(s => s.MarketCap); break;
            }
            if (conf.IsDescending) result = result.Reverse();

            return result;
        }

        public IQueryable<DataAccess.Model.Stock> ApplyPagination(IQueryable<DataAccess.Model.Stock> stocks)
        {
            return (EnablePagination) ? stocks.Skip(calcSkipNumber()).Take(PageSize) : stocks;    
        }

        private int calcSkipNumber()
        {
            return PageSize * (PageNumber - 1);
        }
    }
}