using api.DataAccess.Model;
using api.DataAccess.Repository;
using api.ApplicationService.Dtos.Stock;
using Microsoft.AspNetCore.Mvc;
using api.BusinessLogic.Entity.Stock;
using api.BusinessLogic.Service;
using api.ApplicationService.Dtos.Comment;

namespace api.ApplicationService.Controller
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private StockService stockService;
        private CommentService commentService;
        public StockController(StockService stockService, CommentService commentService) 
        {
            this.stockService = stockService;
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject queryObject) 
        {
            var stocks = await stockService.GetBy(queryObject);
            var stockDtos = stocks.Select(s => new StockDto(s));
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            var targetStock = await stockService.GetBy(id);
            return (targetStock == null) ? NotFound() : Ok(new StockDto(targetStock));
        }

        [HttpGet("with-comments")]
        public async Task<IActionResult> GetAllIncludeComment([FromQuery] StockQueryObject queryObject)
        {
            var stocks = await stockService.GetIncludeCommentBy(queryObject);
            List<StockWithCommenetsDto> stockDtos = new List<StockWithCommenetsDto>();
            foreach (var stock in stocks)
            {
                var comments = await commentService.GetIncludeUserBy(stock.Comments.Select(c => c.Id));
                var commentDtos = comments.Select(c => new CommentWithUserNameDto(c)).ToList();
                stockDtos.Add(new StockWithCommenetsDto(stock, commentDtos));
            }
            return Ok(stockDtos);
        }

        [HttpGet("with-comments/{id}")]
        public async Task<IActionResult> GetByIdIncludeComment([FromRoute] int id)
        {
            var targetStock = await stockService.GetIncludeCommentBy(id);
            if (targetStock == null) return NotFound();

            var comments = await commentService.GetIncludeUserBy(targetStock.Comments.Select(c => c.Id));
            var commentDtos = comments.Select(c => new CommentWithUserNameDto(c)).ToList();
            var result = new StockWithCommenetsDto(targetStock, commentDtos);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] CreateStockDto stockDto)
        {
            Stock newStock = stockDto.ToStockModel();
            await stockService.AddNewStock(newStock);
            return CreatedAtAction(nameof(GetById), new {id = newStock.Id}, newStock.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto stockDto) 
        {
            Stock tempStock = stockDto.ToStockModel(id);
            Stock? updatedStock = await stockService.UpdateStock(tempStock);
            return (updatedStock == null) ? NotFound() : Ok(new StockDto(updatedStock));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            Stock? deletedStock = await stockService.DeleteStock(id);
            return (deletedStock == null) ? NotFound() : NoContent();
        }

        [HttpDelete("force/{id}")]
        public async Task<IActionResult> DeleteWithAllComment([FromRoute] int id)
        {
            Stock? deletedStock = await stockService.DeleteStockWithComment(id);
            return (deletedStock == null) ? NotFound() : NoContent();
        }
    }
}