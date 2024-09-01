namespace api.ApplicationService.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public int UserId { get; set; }

        public CommentDto(DataAccess.Model.Comment commentEntity)
        {
            this.Id = commentEntity.Id;
            this.Title = commentEntity.Title;
            this.Content = commentEntity.Content;
            this.CreatedOn = commentEntity.CreatedOn;
            this.StockId = commentEntity.StockId;
            this.UserId = commentEntity.UserId;
        }
    }
}