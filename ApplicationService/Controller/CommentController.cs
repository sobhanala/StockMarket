using api.ApplicationService.Dtos.Comment;
using api.BusinessLogic.Service;
using api.DataAccess.Model;
using api.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.ApplicationService.Controller
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private CommentRepository commentRepo;
        private StockRepository stockRepo;
        private CommentService commentService;
        public CommentController(CommentRepository commentRepo, StockRepository stockRepo, CommentService commentService)
        {
            this.commentRepo = commentRepo;
            this.stockRepo = stockRepo;
            this.commentService = commentService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await commentService.GetBy();
            var commentDtos = comments.Select(c => new CommentDto(c));
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            var targetComment = await commentService.GetBy(id);
            return (targetComment == null) ? NotFound() : Ok(new CommentDto(targetComment));
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            Comment tempComment = commentDto.ToCommentModel(stockId);
            Comment? addedComment = await commentService.AddComment(stockId, tempComment);
            if (addedComment == null)
            {
                return BadRequest($"the stock id {stockId} does not exist");
            }
            else 
            {
                return CreatedAtAction(nameof(GetById), new { id = addedComment.Id}, addedComment.Id);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
        {
            Comment tempComment = commentDto.ToCommentModel(id);
            Comment? updatedComment = await commentService.UpdateComment(tempComment);
            return (updatedComment == null) ? NotFound() : Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var deletedComment = await commentService.DeleteComment(id);
            return (deletedComment == null) ? NotFound() : NoContent();
        }
    }
}