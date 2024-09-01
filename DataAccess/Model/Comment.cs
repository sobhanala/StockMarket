using System.ComponentModel.DataAnnotations.Schema;

namespace api.DataAccess.Model
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}