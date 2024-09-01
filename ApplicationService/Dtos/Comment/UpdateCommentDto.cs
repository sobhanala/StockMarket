namespace api.ApplicationService.Dtos.Comment
{
    public class UpdateCommentDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DataAccess.Model.Comment ToCommentModel(int id)
        {
            return new DataAccess.Model.Comment()
            {
                Id = id,
                Title = this.Title,
                Content = this.Content,
            };
        }
    }
}