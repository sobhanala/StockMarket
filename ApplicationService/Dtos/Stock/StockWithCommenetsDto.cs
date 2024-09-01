using api.ApplicationService.Dtos.Comment;

namespace api.ApplicationService.Dtos.Stock
{
    public class StockWithCommenetsDto
    {
        public int Id { get; set; } 
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } 
        public List<CommentWithUserNameDto> Comments { get; set; } = new List<CommentWithUserNameDto>();

        public StockWithCommenetsDto(DataAccess.Model.Stock stockModel, List<CommentWithUserNameDto> comments)
        {
            this.Id = stockModel.Id;
            this.Symbol = stockModel.Symbol;
            this.CompanyName = stockModel.CompanyName;
            this.Purchase = stockModel.Purchase;
            this.LastDiv = stockModel.LastDiv;
            this.Industry = stockModel.Industry;
            this.MarketCap = stockModel.MarketCap;
            this.Comments = comments;
        }
    }
}