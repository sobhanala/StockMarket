using System.ComponentModel.DataAnnotations;

namespace api.ApplicationService.Dtos.Comment
{
    public class CreateCommentDto
    {
        [MaxLength(15, ErrorMessage = "Max lenght is 15")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Content is required for comment")]
        public string Content { get; set; } = string.Empty;
        [Required]
        public int UserId { get; set; }

        public DataAccess.Model.Comment ToCommentModel(int stockId)
        {
            return new DataAccess.Model.Comment()
            {
                Title = this.Title,
                Content = this.Content,
                StockId = stockId,
                UserId = this.UserId
            };
        }
    }
}